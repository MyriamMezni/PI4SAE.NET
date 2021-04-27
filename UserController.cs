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
    public class UserController : Controller
    {
       // string Baseurl = "http://localhost:8082/";
        // GET: User
        /* public ActionResult Index()
         {
             HttpClient Client = new HttpClient();
             Client.BaseAddress = new Uri("http://localhost:8082");
             Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/retrieve-all-users").Result;
             if (response.IsSuccessStatusCode)
             {
                 ViewBag.result = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
             }
             else
             {
                 ViewBag.result = "error";
             }
             return View();
         }*/

        public async Task<ActionResult> Index()
        {

            string Baseurl = "http://localhost:8082/";
            List<User> getUser = new List<User>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/retrieve-all-users");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<User>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }

        public async Task<ActionResult> Index4()
        {

            string Baseurl = "http://localhost:8082/";
            List<User> getUser = new List<User>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/retrieve-all-users");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<User>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }

        public async Task<ActionResult> Index5()
        {

            string Baseurl = "http://localhost:8082/";
            List<User> getUser = new List<User>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/retrieve-all-users");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<User>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }

        //POST: Product/Index
        [HttpPost]
        public ActionResult Index(string filtre)
        {
            // var list = productService.GetMany();
            List<User> getUser = new List<User>();
            
            if (!String.IsNullOrEmpty(filtre))
            {
                getUser = getUser.Where(p => p.name.ToString().Equals(filtre)).ToList();
            }
            return View(getUser);
        }




        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            User User = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-user/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<User>();

                User = readTask.Result;
            }
            return View(User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/add-user", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }





        // GET: User/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }*/

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            User User = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-user/"+id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<User>();

                User = readTask.Result;
            }
            return View(User);
        }

        // POST: User/Edit/5
        
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
        [HttpPost]
        public ActionResult Edit(User epm)
        {

            HttpClient client = new HttpClient();
            User User = null;

            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<User>("SpringMVC/servlet/modify-user", epm);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            User User = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-user/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<User>();

                User = readTask.Result;
            }
            return View(User);
            // return View();
        }

        // POST: User/Delete/5
        [HttpPost]        
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                HttpResponseMessage response = Client.DeleteAsync("SpringMVC/servlet/remove-user/" + id.ToString()).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: User/authenticateUser

        
        public async Task<ActionResult> authenticateUser(User epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/authenticateUser", epm);
                if (response.IsSuccessStatusCode)
                {
                    //var type = epm.formation;
                     return RedirectToAction("Index2");

                    //return View(Details);

                }
            }
            return View(epm);
        }





        public async Task<ActionResult> Index3()
        {

            string Baseurl = "http://localhost:8082/";
            List<User> getUser = new List<User>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("SpringMVC/servlet/retrieve-all-users");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    getUser = JsonConvert.DeserializeObject<List<User>>(UserResponse);

                }
                //returning the employee list to view  
                return View(getUser);
            }
        }


        // POST: User/authenticateUser

        public async Task<ActionResult> sendSimpleEmail(User epm)
        {
            string Baseurl = "http://localhost:8082/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("SpringMVC/servlet/sendSimpleEmail", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }



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
                    return RedirectToAction("IndexKinderGartenAdmin");

                    //return View(Details);

                }
            }
            return View(epm);
        }


        // GET: Parent/Details/5
        public ActionResult DetailsParent(int id)
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
        public ActionResult CreateParent()
        {
            return View();
        }

        // POST: Parent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParent(Parent epm)
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

        public ActionResult EditParent(int id)
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

        [HttpPost]
        public ActionResult EditParent(Parent epm)
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
        public ActionResult DeleteParent(int id)
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
        public ActionResult DeleteParent(int id, FormCollection collection)
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
        }*/



    }
}
