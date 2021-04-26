using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webtuto.Models;

namespace Webtuto.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            // Session["auth"] = 15;
            IEnumerable<Category_Products> Cat = null;

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

                    Cat = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..

                    Cat = Enumerable.Empty<Category_Products>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Cat);
        }

    

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            Category_Products Cat = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetCategory/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category_Products>();
                    readTask.Wait();

                    Cat = readTask.Result;
                }
            }
            return View(Cat);
        }

   

    // GET: Category/Create
    public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category_Products Cat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<Category_Products>("AddCategory", Cat);
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

    Category_Products cats = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var responseTask = client.GetAsync("GetCategory/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category_Products>();
                    readTask.Wait();

                    cats = readTask.Result;
                }
            }
            return View(cats);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id , Category_Products cat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var putTask = client.PutAsJsonAsync<Category_Products>("UpdateCategory", cat);
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
                    var deleteTask = client.DeleteAsync("RemoveCategory/" + id.ToString());

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
