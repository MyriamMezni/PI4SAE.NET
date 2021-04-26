using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webtuto.Models;

namespace Webtuto.Controllers
{
    public class PayementController : Controller
    {
        // GET: Payement
        public ActionResult Index()
        {
            return View();
        }

        // GET: Payement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payement/Create
        [HttpPost]
        public ActionResult Create(ChargeRequest chargeRequest)
        {
            string carta = chargeRequest.carta;
            int expMonth = chargeRequest.expMonth;
            int expYear = chargeRequest.expYear;
            string cvc = chargeRequest.cvc;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082/SpringMVC/servlet/");
                var postJob = client.PostAsJsonAsync<ChargeRequest>("pay/"+1+"/"+carta.ToString()+"/"+expMonth.ToString()+"/"+expYear.ToString()+"/"+cvc ,chargeRequest);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("../Item/ListItemPayee");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                //  ViewBag.CategoryList= new SelectList(categorys, "idCategory", "nameCategory");
                return View(chargeRequest);
            }
        }

        // GET: Payement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payement/Edit/5
        [HttpPost]
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
        }

        // GET: Payement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payement/Delete/5
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
    }
}
