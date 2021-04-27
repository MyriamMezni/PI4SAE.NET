using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PI4SAE.Models;
namespace PI4SAE.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetMenu").Result;
            IEnumerable<Menu> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Menu>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetMenu").Result;
            IEnumerable<Menu> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Menu>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }




        public ActionResult Day(string id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/MenuPerDay/"+id.ToString()).Result;
            HttpResponseMessage responseTask = Client.GetAsync("SpringMVC/servlet/Calories/" + id.ToString()).Result;

            String result2;

            IEnumerable<Menu> result;
            if (response.IsSuccessStatusCode)
            {
                 result2 = responseTask.Content.ReadAsStringAsync().Result.ToString(); ;
                ViewData["nbcCalories"] = result2.ToString();

                result = response.Content.ReadAsAsync<IEnumerable<Menu>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }





        [HttpPost]
        public ActionResult Index(string filtre,string id)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetMenu").Result;
            IEnumerable<Menu> result;
            IEnumerable<Menu> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Menu>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindMenuByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Menu>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }







        [HttpPost]
        public ActionResult IndexParent(string filtre, string id)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetMenu").Result;
            IEnumerable<Menu> result;
            IEnumerable<Menu> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Menu>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindMenuByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Menu>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }
        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Menu Menu = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveMenu/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Menu>();

                Menu = readTask.Result;
            }
            return View(Menu);
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,Menu evm)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Menu>("SpringMVC/servlet/addMenu", evm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            Menu Menu = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveMenu/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Menu>();

                Menu = readTask.Result;
            }
            return View(Menu);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Menu evm)
        {
            HttpClient client = new HttpClient();
            Menu Menu = null;

            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync< Menu>("SpringMVC/servlet/modifyMenu/", evm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection,Menu evm)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/removeMenu/" + id.ToString()).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





        public ActionResult Affecter(int id, KinderGarten evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

            ViewBag.idKinderGarten = new SelectList(result, "idKinderGarten", "nameKinderGarten");









            Menu Menu = null;
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = Client.GetAsync("SpringMVC/servlet/retrieveMenu/" + id.ToString());

            var result2 = responseTask.Result;
            if (result2.IsSuccessStatusCode)
            {
                var readTask = result2.Content.ReadAsAsync<Menu>();

                Menu = readTask.Result;
            }
            return View(Menu);


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

            var putTask = Client.PutAsJsonAsync<KinderGarten>("SpringMVC/servlet/affecterMenuAKinderGarten/" + id.ToString() + "/" + evm.idKinderGarten.ToString(), evm);
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
