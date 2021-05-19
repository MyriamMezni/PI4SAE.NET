using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PI4SAE.Models;
using System.Net.Mail;
using System.Net;

namespace PI4SAE.Controllers
{
    public class RdvController : Controller
    {
        HttpClient httpClient;
        string baseAddress;

        public RdvController()
        {
            baseAddress = "http://localhost:8082/SpringMVC/servlet/";
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // GET: Rdv
        public ActionResult Index()
        {
            HttpResponseMessage response = httpClient.GetAsync("rdvs").Result;
            IEnumerable<Rdv> rdvs = response.Content.ReadAsAsync<IEnumerable<Rdv>>().Result;
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(rdvs));
            ViewBag.rdvs = rdvs;

            HttpResponseMessage response2 = httpClient.GetAsync("rdvStat").Result;
            IEnumerable<RdvStat> rdvStat = response2.Content.ReadAsAsync<IEnumerable<RdvStat>>().Result;
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(rdvStat));
            ViewBag.rdvStat = rdvStat;

            return View();
        }


        public ActionResult Delete(int id)
        {
            var APIResponse = httpClient.DeleteAsync(httpClient.BaseAddress + "rdvs/" + id);
            APIResponse.Wait();
            return RedirectToAction("Index");
        }

        public ActionResult IndexParent()
        {
            int idParent = 1;

            HttpResponseMessage response = httpClient.GetAsync("myRdvs/" + idParent).Result;
            IEnumerable<Rdv> rdvs = response.Content.ReadAsAsync<IEnumerable<Rdv>>().Result;
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(rdvs));
            ViewBag.rdvs = rdvs;

            return View();
        }

        public ActionResult Valider(int id)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("achref.mahmoud@esprit.tn", "pcdvd123456789");
            MailMessage msgObj = new MailMessage();
            msgObj.From=new MailAddress("KinderGarten@gmail.com");
            msgObj.To.Add("achraf096@gmail.com");
            msgObj.Subject= "Rendez-vous";
            msgObj.Body = "Votre rendez vous a ete accepte";
            client.Send(msgObj);

            Rdv rdv = new Rdv();
            var APIResponse = httpClient.PutAsJsonAsync<Rdv>(httpClient.BaseAddress + "rdv/" + id, rdv);
            APIResponse.Wait();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            HttpResponseMessage response = httpClient.GetAsync("kindergartens").Result;
            IEnumerable<KinderGarten> kinderGartens = response.Content.ReadAsAsync<IEnumerable<KinderGarten>>().Result;

            ViewBag.kinderGartens = new SelectList(kinderGartens, "idKinderGarten", "nameKinderGarten");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Rdv rdv)
        {
            KinderGarten kg = new KinderGarten();
            kg.idKinderGarten = rdv.idkg;
            rdv.kinderGarten = kg;
            User p = new User();
            p.idUser = 1;
            rdv.parent = p;
            var APIResponse = httpClient.PostAsJsonAsync<Rdv>(baseAddress + "rdvs", rdv).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            APIResponse.Wait();
            return RedirectToAction("Index");
        }
    }
}