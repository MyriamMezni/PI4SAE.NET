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
    public class ParentController : Controller
    {



        public ActionResult IndexMail()
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/sendSimpleEmail").Result;
            IEnumerable<Parent> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;
            }
            else
            { result = null; }
            return View(result);
        }
        // GET: Parent
        /*    public async Task<ActionResult> IndexParent()
            {

                string Baseurl = "http://localhost:8082/";
                List<Parent> getUser = new List<Parent>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        getUser = JsonConvert.DeserializeObject<List<Parent>>(UserResponse);

                    }
                    //returning the employee list to view  
                    return View(getUser);
                }
            }*/
        //Get Parent
        public ActionResult IndexParent()
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent").Result;
            IEnumerable<Parent> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;
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
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent").Result;
            IEnumerable<Parent> result;
            IEnumerable<Parent> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindParentByNameParent/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Parent>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }



        public ActionResult IndexParent2()
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent").Result;
            IEnumerable<Parent> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;
            }
            else
            { result = null; }
            return View(result);
        }



        [HttpPost]
        public ActionResult IndexParent2(string filtre)
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent").Result;
            IEnumerable<Parent> result;
            IEnumerable<Parent> result2;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<IEnumerable<Parent>>().Result;

                if (!String.IsNullOrEmpty(filtre))
                {
                    HttpResponseMessage response2 = Client.GetAsync("SpringMVC/servlet/FindParentByNameParent/" + filtre.ToString()).Result;
                    result = response2.Content.ReadAsAsync<IEnumerable<Parent>>().Result;

                    // list = list.Where(p => p.Name.ToString().Equals(filtre)).ToList();
                }
            }
            else
            { result = null; }

            return View(result);
        }


        public async Task<ActionResult> Index2(Parent p)
        {

            string Baseurl = "http://localhost:8082/";
            List<Parent> getUser = new List<Parent>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/GetUserSortedByTypeParent");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<Parent>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }

        // GET: Parent/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Parent Parent = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-parent/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Parent>();

                Parent = readTask.Result;
            }
            return View(Parent);
        }

        // GET: Parent/Create
        /*     public ActionResult Create()
             {
                 return View();
             }*/
        // GET: Parent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Parent epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/add-parent", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexParent");
                }
            }
            return View(epm);
        }
        // GET: Parent/Edit/5
        /*    public ActionResult Edit(int id)
            {
                return View();
            }*/

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            Parent Parent = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-parent/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Parent>();

                Parent = readTask.Result;
            }
            return View(Parent);
        }

        // POST: Parent/Edit/5
        /*  [HttpPost]
          public ActionResult Edit(int id, FormCollection collection)
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

        [HttpPost]
        public ActionResult Edit(Parent epm)
        {
            HttpClient client = new HttpClient();
            Parent Parent = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP POST
            var putTask = client.PutAsJsonAsync<Parent>("SpringMVC/servlet/modify-parent", epm);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexParent");
            }
            return View();
        }

        // GET: Parent/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            Parent Parent = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-parent/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Parent>();

                Parent = readTask.Result;
            }
            return View(Parent);
            //return View();
        }





        // POST: Parent/Delete/5
        [HttpPost]
        /*   public ActionResult Delete(int id, FormCollection collection)
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

                return RedirectToAction("IndexParent");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> authenticateParent(Parent epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/authenticateParent", epm);
                if (response.IsSuccessStatusCode)
                {

                    //  Session["UserConnecte"] = epm.idUser;

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








                    return RedirectToAction("Index2");

                    //return View(Details);

                }
            }
            return View(epm);
        }












    }
}
