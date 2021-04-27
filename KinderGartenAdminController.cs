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
    public class KinderGartenAdminController : Controller
    {
        // GET: KinderGartenAdmin
        /* public async Task<ActionResult> IndexKinderGartenAdmin()
         {

             string Baseurl = "http://localhost:8082/";
             List<KinderGartenAdmin> getUser = new List<KinderGartenAdmin>();

             using (var client = new HttpClient())
             {
                 //Passing service base url  
                 client.BaseAddress = new Uri(Baseurl);

                 client.DefaultRequestHeaders.Clear();
                 //Define request data format  
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                 //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                 HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeKinderGartenAdmin");
                 if (Res.IsSuccessStatusCode)
                 {
                     //Storing the response details recieved from web api   
                     var UserResponse = Res.Content.ReadAsStringAsync().Result;

                     //Deserializing the response recieved from web api and storing into the Employee list  
                     getUser = JsonConvert.DeserializeObject<List<KinderGartenAdmin>>(UserResponse);

                 }
                 //returning the employee list to view  
                 return View(getUser);
             }
         }*/

        public ActionResult IndexKinderGartenAdmin()
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeKinderGartenAdmin").Result;
            IEnumerable<KinderGartenAdmin> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<KinderGartenAdmin>>().Result;
            }
            else
            { result = null; }
            return View(result);
        }

        [HttpPost]
        public ActionResult IndexKinderGartenAdmin(string filtre)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeKinderGartenAdmin").Result;
            IEnumerable<KinderGartenAdmin> result;
            IEnumerable<KinderGartenAdmin> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<KinderGartenAdmin>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindKinderGartenAdminByNameKinderGartenAdmins/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<KinderGartenAdmin>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }


        // GET: KinderGartenAdmin/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            KinderGartenAdmin KinderGartenAdmin = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-kinderGartenAdmin/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<KinderGartenAdmin>();

                KinderGartenAdmin = readTask.Result;
            }
            return View(KinderGartenAdmin);
        }

        // GET: KinderGartenAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KinderGartenAdmin epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/add-KinderGartenAdmin", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexKinderGartenAdmin");
                }
            }
            return View(epm);
        }

        // GET: KinderGartenAdmin/Edit/5
        /* public ActionResult Edit(int id)
         {
             return View();
         }*/

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            KinderGartenAdmin KinderGartenAdmin = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-kinderGartenAdmin/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<KinderGartenAdmin>();

                KinderGartenAdmin = readTask.Result;
            }
            return View(KinderGartenAdmin);
        }

        // POST: KinderGartenAdmin/Edit/5
        [HttpPost]
        /*public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        public ActionResult Edit(KinderGartenAdmin epm)
        {

            HttpClient client = new HttpClient();
            KinderGartenAdmin KinderGartenAdmin = null;

            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<KinderGartenAdmin>("SpringMVC/servlet/modify-KinderGartenAdmin", epm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("IndexKinderGartenAdmin");

            }
            return View();
        }

        // GET: KinderGartenAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            KinderGartenAdmin KinderGartenAdmin = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-kinderGartenAdmin/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<KinderGartenAdmin>();

                KinderGartenAdmin = readTask.Result;
            }
            return View(KinderGartenAdmin);
            // return View();
        }

        // POST: KinderGartenAdmin/Delete/5
        [HttpPost]
        /*  public ActionResult Delete(int id, FormCollection collection)
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
          }*/

        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/remove-user/" + id.ToString()).Result;

                return RedirectToAction("IndexKinderGartenAdmin");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> authenticateKinderGartenAdmin(KinderGartenAdmin epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/authenticateKinderGartenAdmin", epm);
                if (response.IsSuccessStatusCode)
                {
                    //var type = epm.formation;
                    string name = epm.name;
                    User User = null;
                    var responseTask = client.GetAsync("SpringMVC/servlet/getUserByName/" + name.ToString());

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<User>();

                        User = readTask.Result;
                    }

                    Session["UserConnecte"] = User;
                    Session["UserConnecteId"] = User.idUser;
                    Session["UserConnecteName"] = User.name;

                    return RedirectToAction("IndexKinderGartenAdmin");

                    //return View(Details);

                }
            }
            return View(epm);
        }
   
        public async Task<ActionResult> Index2()
        {

            string Baseurl = "http://localhost:8082/";
            List<KinderGartenAdmin> getUser = new List<KinderGartenAdmin>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeKinderGartenAdmin");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<KinderGartenAdmin>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }
    }
}
