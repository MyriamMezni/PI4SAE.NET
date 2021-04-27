using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PI4SAE.Models;

namespace PI4SAE.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result;
            IEnumerable<Events> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindEventsByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Events>>().Result;

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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result;
            IEnumerable<Events> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindEventsByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Events>>().Result;

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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result;
            IEnumerable<Events> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindEventsByNameKinderGarten/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Events>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }


        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Events Events = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveEvents/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Events>();

                Events = readTask.Result;
            }
            return View(Events);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,Events evm, HttpPostedFileBase file)
        {
            evm.image = file.FileName;

            try
            {


                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Events>("SpringMVC/servlet/AddEvents", evm).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            Events Events = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveEvents/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Events>();

                Events = readTask.Result;
            }
            return View(Events);
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,Events evm, HttpPostedFileBase file)
        {
            evm.image = file.FileName;

            HttpClient client = new HttpClient();
            Activities Events = null;


            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Events>("SpringMVC/servlet/ModifyEvents/", evm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection,Events evm)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/RemoveEvents/" + id.ToString()).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





        public ActionResult Affecter(int id, User evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetKinderGarten").Result;
            IEnumerable<KinderGarten> result;
            result = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

            ViewBag.idKinderGarten = new SelectList(result, "idKinderGarten", "nameKinderGarten");




            Events Events = null;
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = Client.GetAsync("SpringMVC/servlet/retrieveEvents/" + id.ToString());

            var result2 = responseTask.Result;
            if (result2.IsSuccessStatusCode)
            {
                var readTask = result2.Content.ReadAsAsync<Events>();

                Events = readTask.Result;
            }
            return View(Events);


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

            var putTask = Client.PutAsJsonAsync<KinderGarten>("SpringMVC/servlet/affecterEventAKinderGarten/" + id.ToString() + "/" + evm.idKinderGarten.ToString(), evm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }



            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            IEnumerable<Events> result2;
            result2 = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;

            ViewBag.idKinderGarten = new SelectList(result2, "idKinderGarten", "nameKinderGarten");
            return View();

        }













        public ActionResult Participer(int id, Events evm)
        {
              HttpClient Client = new HttpClient();
              Client.BaseAddress = new Uri("http://localhost:8082");
              Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
              HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/retrieve-all-users").Result;
              IEnumerable<User> result;
              result = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            var res=Session["UserConnecte"];    
            // ViewBag.idUser = new SelectList(result, "idUser", "name");







            Events Events = null;
              //HTTP GET("SpringMVC/servlet/GetActivities")
              var responseTask = Client.GetAsync("SpringMVC/servlet/retrieveEvents/" + id.ToString());

              var result2 = responseTask.Result;
              if (result2.IsSuccessStatusCode)
              {
                  var readTask = result2.Content.ReadAsAsync<Events>();

                  Events = readTask.Result;
              }
              return View(Events);
        

        }


        [HttpPost]
        public ActionResult Participer(int id, FormCollection collection, User evm)
        {

            
             HttpClient Client = new HttpClient();
               KinderGarten KinderGarten = null;

               Client.BaseAddress = new Uri("http://localhost:8082");

               //HTTP POST
               Console.WriteLine(id);
               Console.WriteLine(evm.idUser);
           

               var putTask = Client.PutAsJsonAsync<User>("SpringMVC/servlet/UserParticipeEvent/"  +Session["UserConnecteId"].ToString() + "/" + id.ToString(), evm);
               putTask.Wait();

               var result = putTask.Result;
               if (result.IsSuccessStatusCode)
               {

                   return RedirectToAction("Index");

               }



              



            return View();


        }









        public ActionResult CountParticipant(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetEvents").Result;
            HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/NbrParticipantParEvent/"+id.ToString()).Result;
            String result2;

            IEnumerable<Events> result;
            if (response.IsSuccessStatusCode)
            {
                result2 = response2.Content.ReadAsStringAsync().Result.ToString();
                ViewData["nb"] = result2;
                result = response.Content.ReadAsAsync<IEnumerable<Events>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }











          public ActionResult SendMail( Events evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/retrieve-all-users").Result;
            IEnumerable<User> result;
            result = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

            ViewBag.idUser = new SelectList(result, "idUser", "name");







       
            return View();


        }


        [HttpPost]
        public ActionResult SendMail( FormCollection collection, User evm)
        {


            HttpClient Client = new HttpClient();
            KinderGarten KinderGarten = null;

            Client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            Console.WriteLine(evm.idUser);
            Client.GetAsync("SpringMVC/servlet/sendmailForEvent/" + evm.idUser.ToString());
            var putTask = Client.GetAsync("SpringMVC/servlet/sendmailForEvent/" + evm.idUser.ToString());

            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }







            return View();


        }















    }
}

