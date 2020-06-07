using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Entities;
using CustomerManagement.WebApp.Models;
using CustomerManagement.BusinessLayer;
using CustomerManagement.Entities.ModelView;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.WebApp.Filters;

namespace CustomerManagement.WebApp.Controllers
{
    [Exc]
    public class CustomerController : Controller
    {
        private CustomerManager customerManager = new CustomerManager();

        public ActionResult Index()
        {
            return View(customerManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customerManager.Find(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                customerManager.Insert(customer);
                return RedirectToAction("IndexCustomer", "Login");
            }

            return View(customer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = customerManager.Find(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Customer cus = customerManager.Find(x => x.Id == customer.Id);
                cus.Name = customer.Name;
                cus.Surname = customer.Surname;
                cus.MobilePhone = customer.MobilePhone;
                cus.Email = customer.Email;
                cus.Password = customer.Password;
                customerManager.Update(cus);

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customerManager.Find(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = customerManager.Find(x => x.Id == id);
            customerManager.Delete(customer);
            return RedirectToAction("Index");
        }

        public ActionResult CustomerActivate(Guid id)
        {
            ActivationViewModal<string> activationViewModal = new ActivationViewModal<string>();
            BusinessLayerResult<Customer> res = customerManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                activationViewModal.Title = "Etkinleştirme Hatası";
                res.Errors.ForEach(x => activationViewModal.Items.Add(x.Message));

                ViewBag.Result = res.Errors;
                return View(activationViewModal);
            }

            return View("~/Views/CMUser/UserActivate.cshtml", activationViewModal);
        }
    }
}
