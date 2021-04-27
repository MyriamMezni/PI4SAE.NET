using Newtonsoft.Json;
using PI4SAE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PI4SAE.Controllers
{
    public class KidController : Controller
    {
        // GET: Kid
        public async Task<ActionResult> IndexKid()
        {

            string Baseurl = "http://localhost:8082/";
            List<Kid> getUser = new List<Kid>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/FindKidSortedByNameKid");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<Kid>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }

        // GET: Kid/Details/5

        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Kid Kid = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveKid/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Kid>();

                Kid = readTask.Result;
            }
            return View(Kid);
        }

        // GET: Kid/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kid/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Kid epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/AddKid", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexKid");
                }
            }
            return View(epm);
        }

        // GET: Kid/Edit/5
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            Kid Kid = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveKid/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Kid>();

                Kid = readTask.Result;
            }
            return View(Kid);
        }

        // POST: Kid/Edit/5
        [HttpPost]
        public ActionResult Edit(Kid epm)
        {
            HttpClient client = new HttpClient();
            Kid Kid = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP POST
            var putTask = client.PutAsJsonAsync<Kid>("SpringMVC/servlet/ModifyKid", epm);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexKid");
            }
            return View();
        }

        // GET: Kid/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            Kid Kid = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieveKid/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Kid>();

                Kid = readTask.Result;
            }
            return View(Kid);
            //return View();
        }

        // POST: Kid/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/RemoveKid/" + id.ToString()).Result;

                return RedirectToAction("IndexKid");
            }
            catch
            {
                return View();
            }
        }











        public ActionResult Affecter(int id, Parent evm)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent").Result;
            IEnumerable<Parent> result;
            result = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;

            ViewBag.idUser = new SelectList(result, "idUser", "name");

            Kid Activities = null;
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = Client.GetAsync("SpringMVC/servlet/retrieveKid/" + id.ToString());

            var result2 = responseTask.Result;
            if (result2.IsSuccessStatusCode)
            {
                var readTask = result2.Content.ReadAsAsync<Kid>();

                Activities = readTask.Result;
            }
            return View(Activities);
        }

        [HttpPost]
        public ActionResult Affecter(int id, FormCollection collection, Parent evm)
        {

            HttpClient Client = new HttpClient();
            Parent KinderGarten = null;

            Client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            Console.WriteLine(id);
            Console.WriteLine(evm.idUser);

            var putTask = Client.PutAsJsonAsync<Parent>("SpringMVC/servlet/affecterKidAParent/" + id.ToString() + "/" + evm.idUser.ToString(), evm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexKid");
            }

            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent").Result;
            IEnumerable<Parent> result2;
            result2 = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;

            ViewBag.idKinderGarten = new SelectList(result2, "idUser", "name");
            return View();

        }








    }
}
