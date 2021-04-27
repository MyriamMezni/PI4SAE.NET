using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PI4SAE.Models;

namespace PI4SAE.Controllers
{
    public class KinderGartenController : Controller
    {
        // GET: KinderGarten
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable <KinderGarten> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }


        [HttpPost]
        public ActionResult Index(string filtre)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            IEnumerable<KinderGarten> result2;

            if (response.IsSuccessStatusCode) 
            {
                result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindKinderGartenByNameKinderGarten/"+filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                   // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }








        public ActionResult IndexParent()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }


        [HttpPost]
        public ActionResult IndexParent(string filtre)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            IEnumerable<KinderGarten> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindKinderGartenByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }















        public ActionResult IndexVisiteur()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }


        [HttpPost]
        public ActionResult IndexVisiteur(string filtre)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            IEnumerable<KinderGarten> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindKinderGartenByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }


        // GET: KinderGarten/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            KinderGarten KinderGarten = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/RetrieveKinderGarten/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<KinderGarten>();

                KinderGarten = readTask.Result;
            }
            return View(KinderGarten);
        }

        // GET: KinderGarten/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KinderGarten/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,KinderGarten evm)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<KinderGarten>("SpringMVC/servlet/AddKinderGarten", evm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KinderGarten/Edit/5
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            KinderGarten KinderGarten = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/RetrieveKinderGarten/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<KinderGarten>();

                KinderGarten = readTask.Result;
            }
            return View(KinderGarten);
        }

        // POST: KinderGarten/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,KinderGarten evm)
        {
            HttpClient client = new HttpClient();
            KinderGarten KinderGarten = null;

            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<KinderGarten>("SpringMVC/servlet/ModifyKinderGarten/", evm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: KinderGarten/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KinderGarten/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection, KinderGarten evm)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/removeKinderGarten/" + id.ToString()).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
