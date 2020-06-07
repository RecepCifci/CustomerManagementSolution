using CustomerManagement.BusinessLayer;
using CustomerManagement.Entities;
using CustomerManagement.WebApp.Filters;
using CustomerManagement.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.WebApp.Areas.CustomerArea.Controllers
{
    [Exc]
    public class IncidentController : Controller
    {
        private IncidentManager incidentManager = new IncidentManager();
        private CategoryManager categoryManager = new CategoryManager();
        private CustomerManager customerManager = new CustomerManager();
        private CMUserManager cmUserManager = new CMUserManager();
        private ProductManager productManager = new ProductManager();

        LoginManager loginManager = new LoginManager();
        private Customer CurrentCustomer = CurrentSession.Customer;
        public ActionResult Index()
        {
            //loginManager.initdb();

            //if (CurrentCustomer == null)
            //{
            //    CurrentCustomer = customerManager.Find(x => x.Id == 1);
            //    CurrentSession.Set<Customer>("logincustomer", CurrentCustomer);
            //}
            var incidents = incidentManager.ListQueryable().Where(i => i.Customer.Id == CurrentCustomer.Id)
                                                       .Include(i => i.Category).Include(i => i.Customer).Include(i => i.Owner).Include(i => i.Product).ToList();
            return View(incidents.ToList());
        }

        public ActionResult Create()
        {
            List<Customer> customerSelectedList = customerManager.List(x => x.Id == CurrentCustomer.Id);
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(customerSelectedList, "Id", "Fullname", "Surname");
            ViewBag.OwnerId = new SelectList(cmUserManager.List(), "Id", "Fullname", "Surname");
            ViewBag.ProductId = new SelectList(productManager.List(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Incident incident)
        {
            incident.StartDate = DateTime.Now;
            incident.EndDate = DateTime.Now.AddDays(7);
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                incidentManager.Insert(incident);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(new List<Customer> { CurrentCustomer }, "Id", "Fullname", "Surname");
            ViewBag.OwnerId = new SelectList(cmUserManager.List(), "Id", "Fullname", "Surname");
            ViewBag.ProductId = new SelectList(productManager.List(), "Id", "Name");
            return View(incident);
        }
    }
}