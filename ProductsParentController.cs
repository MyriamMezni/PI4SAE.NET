using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webtuto.Models;

namespace PI4SAE.Controllers
{
    public class ProductsParentController : Controller
    {
        public ActionResult ListProductsParent()
        {

            // Session["auth"] = 15;
            IEnumerable<Products> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetAllProducts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Products>>();
                    readTask.Wait();

                    products = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<Products>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }


        public ActionResult ListProducts()
        {

            // Session["auth"] = 15;
            IEnumerable<Products> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetAllProducts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Products>>();
                    readTask.Wait();

                    products = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<Products>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }

        [HttpPost]
        public ActionResult ListProducts(string filtre)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetAllProducts").Result;
            IEnumerable<Products> result;


            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Products>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/GetProductsbyName/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Products>>().Result;


                }
            }
            else
            { result = null; }

            return View(result);
        }


        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            Products products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetProduct/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Products>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }

        // GET: Products/Create
        

       
       



      


        public ActionResult AddToCart(int id)
        {
            Products products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetProduct/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Products>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }


        [HttpPost]
        public ActionResult AddToCart(int id, int quantity, Cart cart)
        {

            Products prod = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetProduct/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Products>();
                    readTask.Wait();

                    prod = readTask.Result;
                }
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var putTask = client.PostAsJsonAsync<Cart>("AddCart/" + id.ToString() + "/" + Session["UserConnecteId"].ToString() + "/" + quantity.ToString(), cart);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListProductsParent");

                }


                return View(prod);
            }

        }

        public ActionResult HighestPrice()
        {

            IEnumerable<Products> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetProductsbyPrixDESC");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Products>>();
                    readTask.Wait();

                    products = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<Products>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }
        public ActionResult LowestPrice()
        {

            IEnumerable<Products> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetProductsbyPrixASC");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Products>>();
                    readTask.Wait();

                    products = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<Products>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }































    }
}

