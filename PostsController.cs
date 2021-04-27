using Pi4Sae4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Pi4Sae4.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/getPosts").Result;
            IEnumerable<Posts> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Posts>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }



        public ActionResult IndexAdmin()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/getPosts").Result;
            IEnumerable<Posts> result;
            if (response.IsSuccessStatusCode)
            {

                result = response.Content.ReadAsAsync<IEnumerable<Posts>>().Result;
            }
            else
            { result = null; }


            return View(result);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/getPostsParId/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();

                Posts = readTask.Result;
            }
            return View(Posts);
        }

        public ActionResult DetailsAdmin(int id)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/getPostsParId/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();

                Posts = readTask.Result;
            }
            return View(Posts);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        public ActionResult Create(Posts Pst, HttpPostedFileBase file)
        {

            Pst.img = file.FileName;
            try
            {

                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Posts>("SpringMVC/servlet/addPosts/"+Session["UserConnecteId"].ToString(), Pst).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }









        // GET: Posts/Create
        public ActionResult CreateAdmin()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        public ActionResult CreateAdmin(Posts Pst, HttpPostedFileBase file)
        {

            Pst.img = file.FileName;
            try
            {

                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082");
                Client.PostAsJsonAsync<Posts>("SpringMVC/servlet/addPostsAdmin/" + Session["UserConnecteId"].ToString(), Pst).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("IndexAdmin");
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
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/getPostsParId/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();

                Posts = readTask.Result;
            }
            return View(Posts);
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Posts pst, HttpPostedFileBase file)
        {
            pst.img = file.FileName;

            HttpClient client = new HttpClient();
            Posts p = null;


            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/modifyPost/" + Session["UserConnecteId"].ToString(), pst);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }



















        public ActionResult EditAdmin(int id)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/getPostsParId/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();

                Posts = readTask.Result;
            }
            return View(Posts);
        }

        [HttpPost]
        public ActionResult EditAdmin(int id, FormCollection collection, Posts pst, HttpPostedFileBase file)
        {
            pst.img = file.FileName;

            HttpClient client = new HttpClient();
            Posts p = null;


            client.BaseAddress = new Uri("http://localhost:8082");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/modifyPostAdmin/" + Session["UserConnecteId"].ToString(), pst);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            return View();
        }



        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var deleteTask = client.DeleteAsync("removePosts/" + id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexAdmin");
                }
                return RedirectToAction("IndexAdmin");
            }
        }

        // POST: Posts/Delete/5
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







        public ActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comments cmt, int id)
        {


            try
            {

                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                Client.PostAsJsonAsync<Comments>("addComments/" + id.ToString(), cmt).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Details/"+ id.ToString());
            }
            catch
            {
                return View();
            }
        }




        
        public ActionResult TranslatePost(int id)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/TranslatePost/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                string readTask = result.Content.ReadAsStringAsync().Result.ToString();
                ViewData["NvPost"] = readTask;
                ViewData["iddd"] = id;
            }
            return View();
        }




        public String CountReacts(int id)
        {
            String readTask = "";
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/Reacts/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                 readTask = result.Content.ReadAsStringAsync().Result.ToString();
                ViewData["Like&Dislike"] = readTask;
                ViewData["iddd"] = id;
            }
            return readTask;
        }

        public ActionResult BadwordsFilter(int id,Posts p)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
           
            var responseTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/BadWordFilter/" + id.ToString(), p);

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {

                String readTask = result.Content.ReadAsStringAsync().Result.ToString();
                ViewData["bad"] = readTask;
                ViewData["iddd"] = id;

            }
            return View("");
        }

        public ActionResult SendMail(int id)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.GetAsync("SpringMVC/servlet/sendmail/" + Session["UserConnecteId"].ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                string readTask = result.Content.ReadAsStringAsync().Result.ToString();
               

                ViewData["mail"] = readTask;
                ViewData["iddd"] = id;
            }
            return RedirectToAction("IndexAdmin");
        }

        public ActionResult RemoveSignaled(int id)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.DeleteAsync("SpringMVC/servlet/removeSignaledPost/" + id.ToString());

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                string readTask = result.Content.ReadAsStringAsync().Result.ToString();


                ViewData["SignaledPost"] = readTask;
                ViewData["iddd"] = id;
            }
            return View("");
        }



        public ActionResult Like(int id,Posts p)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/Like/" + id.ToString(),p);

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();


             
            }
            return RedirectToAction("Details/" +id.ToString());
        }



        public ActionResult DisLike(int id, Posts p)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/Dislike/" + id.ToString(), p);

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();



            }
            return RedirectToAction("Details/" + id.ToString());
        }

        public ActionResult Signal(int id, Posts p)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/Signaler/" + id.ToString(), p);

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();



            }
            return RedirectToAction("Details/" + id.ToString());
        }







        public ActionResult Signaler(int id, Posts p)
        {
            HttpClient client = new HttpClient();
            Posts Posts = null;
            client.BaseAddress = new Uri("http://localhost:8082");
            //HTTP GET("SpringMVC/servlet/GetActivities")
            var responseTask = client.PutAsJsonAsync<Posts>("SpringMVC/servlet/Signaler/" + id.ToString(), p);

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Posts>();



            }
            return View();
        }
        public ActionResult ViewComments(int id)
        {

            HttpClient Client = new HttpClient();

            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("SpringMVC/servlet/getCommentsofPosts/"+id.ToString()).Result;
            IEnumerable<String> result;


           
            if (response.IsSuccessStatusCode)
            {
               

                result = response.Content.ReadAsAsync<IEnumerable<String>>().Result;
                ViewData["Comments"] = result;

            }
            else
            { result = null; }


            return View(result);
        }
    }
}
