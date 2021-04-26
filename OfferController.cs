using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webtuto.Models;

namespace Webtuto.Controllers
{
    public class OfferController : Controller
    {
        public ActionResult Index()
        {
            // Session["auth"] = 15;
            IEnumerable<OfferProducts> Offer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetAllOffer");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OfferProducts>>();
                    readTask.Wait();

                    Offer = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    Offer = Enumerable.Empty<OfferProducts>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Offer);
        }



        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            OfferProducts Offer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetOffer/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OfferProducts>();
                    readTask.Wait();

                    Offer = readTask.Result;
                }
            }
            return View(Offer);
        }



        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(OfferProducts Cat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<OfferProducts>("AddOffer", Cat);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                //  ViewBag.CategoryList= new SelectList(categorys, "idCategory", "nameCategory");
                return View(Cat);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {

            OfferProducts cats = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetOffer/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OfferProducts>();
                    readTask.Wait();

                    cats = readTask.Result;
                }
            }
            return View(cats);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OfferProducts cat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var putTask = client.PutAsJsonAsync<OfferProducts>("UpdateOffer", cat);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(cat);

            }
        }

        // GET: Category/Delete/5


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var deleteTask = client.DeleteAsync("RemoveOffer/" + id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        // POST: Category/Delete/5

    }

}
