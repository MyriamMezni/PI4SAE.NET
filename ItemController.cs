using PI4SAE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webtuto.Models;

namespace Webtuto.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult ListItem(User evm)
        {
            float Totale = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetMontantTotalConfirmer/"+Session["UserConnecteId"]);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<float>();
                    readTask.Wait();

                    Totale = readTask.Result;
                }
            }
            ViewBag.Totale = Totale;
            IEnumerable<ItemList> itemLists = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetAllItem/" + Session["UserConnecteId"]);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ItemList>>();
                    readTask.Wait();

                    itemLists = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    itemLists = Enumerable.Empty<ItemList>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(itemLists);
        }

        



        public ActionResult ListItemPayee()
        {
            IEnumerable<ItemList> itemLists = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetItemPayee/"+Session["UserConnecteId"]);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ItemList>>();
                    readTask.Wait();

                    itemLists = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    itemLists = Enumerable.Empty<ItemList>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(itemLists);
        }





        // GET: Item/Details/5
        
        public ActionResult ConfirmItem(int id)
        {

            


            Console.WriteLine(id);
            ItemList itemList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var PutTask = client.PutAsJsonAsync<ItemList>("ConfirmLigneItem/" + id.ToString(), itemList);

                var result = PutTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListItem");
                }
                return RedirectToAction("ListItem");
            }
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            ItemList itemList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetItem/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ItemList>();
                    readTask.Wait();

                    itemList = readTask.Result;
                }
            }
            return View(itemList);
        }
    

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,ItemList itemList)
        {

          int  quantity = itemList.quantity;
            ItemList it = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetItem/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ItemList>();
                    readTask.Wait();

                    it = readTask.Result;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var putTask = client.PutAsJsonAsync<ItemList>("updateLigneItem/" + id.ToString() + "/"  +Session["UserConnecteId"] + "/" + quantity.ToString(), itemList);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("ListItem");
                return View(itemList);

            }
        }




        public ActionResult MostPopularProducts()
        {

            IEnumerable<ItemList> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetAllMostpopularProducts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ItemList>>();
                    readTask.Wait();

                    products = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ItemList>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }





        // POST: Item/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var deleteTask = client.DeleteAsync("RemoveLigneItem/" + id.ToString()+"/"+Session["UserConnecteId"]);

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListItem");
                }
                return RedirectToAction("ListItem");
            }
        }
    }
}
