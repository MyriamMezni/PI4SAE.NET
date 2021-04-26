using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webtuto.Models;

namespace Webtuto.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products

        //Hosted web API REST Service base url




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
        public ActionResult Create()
        {
            IEnumerable<Category_Products> categorys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                //HTTP GET
                var responseTask = client.GetAsync("GetAllCategory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Category_Products>>();
                    readTask.Wait();

                    categorys = readTask.Result;
                    

                }
                ViewBag.CategoryList= new SelectList(categorys, "idCategory", "nameCategory");

                return View();
                
                
            }
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult create(Products prod, HttpPostedFileBase file)
        {
            prod.image = file.FileName;
            string idCat = Request.Form["CategoryList"].ToString();
            Console.WriteLine(idCat);
            Category_Products category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetCategory/" + idCat);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category_Products>();
                    readTask.Wait();

                    category = readTask.Result;
                    Console.WriteLine(category);
                }
            }

            prod.category = category;
            Console.WriteLine(prod.category);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<Products>("AddProductwithcat", prod);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListProducts");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                //  ViewBag.CategoryList= new SelectList(categorys, "idCategory", "nameCategory");
                return View(prod);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            Products products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetProduct/" +id.ToString());
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

        //craete post  method to update the data
        [HttpPost]
        public ActionResult Edit(Products product, HttpPostedFileBase file)
        {
            product.image = file.FileName;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var putTask = client.PutAsJsonAsync<Products>("UpdateProducts", product);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("ListProducts");
                return View(product);

            }
        }
        // GET: Products/Delete/5
       public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var deleteTask = client.DeleteAsync("RemoveProducts/"+id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListProducts");
                }
                return RedirectToAction("ListProducts");
            }
        }



        public ActionResult AddOffer( )
        {
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
                ViewBag.OfferList = new SelectList(Offer, "idOffer", "valeur");
                
                return View();




            }
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult AddOffer(int id)
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
          

            string idOffer = Request.Form["OfferList"].ToString();
            Console.WriteLine(idOffer);
            OfferProducts offer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetOffer/" +idOffer);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OfferProducts>();
                    readTask.Wait();

                    offer = readTask.Result;
                    Console.WriteLine(offer);
                }
            }

             Console.WriteLine(prod);
          //  prod.offerProducts = offer;
           
            Console.WriteLine(prod.offerProducts);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var putTask = client.PutAsJsonAsync<Products>("affecterProduitAsOfferValue/"+id.ToString()+"/"+ idOffer, prod);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListProducts");

                }


                return View(prod);
            }
            
        }



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
        public ActionResult AddToCart(int id, int quantity,Cart cart)
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
                var putTask = client.PostAsJsonAsync<Cart>("AddCart/" + id.ToString() + "/" +1+"/"+quantity.ToString(), cart);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListProducts");

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
