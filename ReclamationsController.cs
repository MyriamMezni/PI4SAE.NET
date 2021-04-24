using ReclamationPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ReclamationPI.Controllers
{
    public class ReclamationsController : Controller
    {
        // GET: Reclamations
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/reclamations").Result;
            IEnumerable<Reclamation> result=null;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Reclamation>>().Result;
            }
            else
            { result = null; }

            return View(result);
        }

        // GET: Reclamations/Details/5
        //FIND BY ID
        public ActionResult Details(int idReclamation) 
        {
            HttpClient client = new HttpClient();
            Reclamation reclamation = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/findReclamationById/" + idReclamation.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Reclamation>();

                reclamation = readTask.Result;
            }

            return View(reclamation);
        }

        // GET: Reclamations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reclamations/Create
        [HttpPost]
        public ActionResult Create(Reclamation reclamation)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Reclamation>("SpringMVC/servlet/addReclamation", reclamation).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reclamations/Edit/5
        public ActionResult Edit(int idReclamation)
        {
            HttpClient client = new HttpClient();
            Reclamation reclamation = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            
            var responseTask = client.GetAsync("SpringMVC/servlet/findReclamationById/" + idReclamation.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Reclamation>();

                reclamation = readTask.Result;
            }

            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Reclamation reclamation)
        {
            HttpClient client = new HttpClient();
            Reclamation reclamation1 = null;

            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Reclamation>("SpringMVC/servlet/update/", reclamation);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: Reclamations/Delete/5
        public ActionResult Delete(int idReclamation)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var deleteTask = client.DeleteAsync("delete/" + idReclamation.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }

        // POST: Reclamations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
