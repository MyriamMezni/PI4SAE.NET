using Pi4Sae4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Pi4Sae4.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult Index()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/getComments").Result;
            IEnumerable<Comments> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Comments>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Comments Comments = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/getCommentsParId/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Comments>();

                Comments = readTask.Result;
            }
            return View(Comments);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create(Comments cmt,int id)
        {

           
            try
            {

                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Comments>("SpringMVC/servlet/addComments" + id.ToString(), cmt).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            Comments Comments = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/getCommentsParId/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Comments>();

                Comments = readTask.Result;
            }
            return View(Comments);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Comments  cmt, FormCollection collection)
        {
            HttpClient client = new HttpClient();
            Comments Comments = null;

            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Comments>("SpringMVC/servlet/modifyComment/", cmt);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/removeComments/" + id.ToString()).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
