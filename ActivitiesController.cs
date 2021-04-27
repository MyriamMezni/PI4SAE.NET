using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PI4SAE.Models;

namespace PI4SAE.Controllers
{
    public class ActivitiesController : Controller
    {
        // GET: Activities
        public ActionResult Index()
        {
           

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetActivities").Result;
            IEnumerable<Activities> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Activities>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetActivities").Result;
            IEnumerable<Activities> result;
            IEnumerable<Activities> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Activities>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindActivitiesByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Activities>>().Result;

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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetActivities").Result;
            IEnumerable<Activities> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Activities>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetActivities").Result;
            IEnumerable<Activities> result;
            IEnumerable<Activities> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Activities>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindActivitiesByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Activities>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }








        // GET: Activities/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Activities Activities = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveActivity/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Activities>();

                Activities = readTask.Result;
            }
            return View(Activities);
        }

        // GET: Activities/Create
        public ActionResult Create(KinderGarten evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
           result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

                ViewBag.idKinderGarten = new SelectList(result, "idKinderGarten", "nameKinderGarten");
           
            return View();
        }

        // POST: Activities/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,Activities evm)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Activities>("SpringMVC/servlet/AddActivity",evm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Activities/Edit/5
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            Activities Activities = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveActivity/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Activities>();

                Activities = readTask.Result;
            }
            return View(Activities);
        }

        // POST: Activities/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,Activities evm)
        {
            
                HttpClient client = new HttpClient();
                Activities Activities = null;

                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Activities>("SpringMVC/servlet/ModifyActivity/", evm);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
            return View();

        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            Activities Activities = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveActivity/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Activities>();

                Activities = readTask.Result;
            }
            return View(Activities);
        }

        // POST: Activities/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                   HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/RemoveActivity/"+id.ToString()).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      

        public ActionResult Affecter(int id,KinderGarten evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

            ViewBag.idKinderGarten = new SelectList(result, "idKinderGarten", "nameKinderGarten");








          
            Activities Activities = null;
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask =Client.GetAsync("SpringMVC/servlet/retrieveActivity/" + id.ToString());

            var result2 = responseTask.Result;
            if (result2.IsSuccessStatusCode)
            {
                var readTask = result2.Content.ReadAsAsync<Activities>();

                Activities = readTask.Result;
            }
            return View(Activities);


        }


        [HttpPost]
        public ActionResult Affecter(int id, FormCollection collection, KinderGarten evm)
        {

            HttpClient Client = new HttpClient();
            KinderGarten KinderGarten = null;

            Client.BaseAddress = new Uri("http://localhost:8082");
          
            //HTTP POST
            Console.WriteLine(id);
            Console.WriteLine(evm.idKinderGarten);

            var putTask = Client.PutAsJsonAsync<KinderGarten>("SpringMVC/servlet/affecterActivityAKinderGarten/"+id.ToString()+"/"+evm.idKinderGarten.ToString(), evm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }



            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result2;
            result2 = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

            ViewBag.idKinderGarten = new SelectList(result2, "idKinderGarten", "nameKinderGarten");
            return View();

        }

    }
}
