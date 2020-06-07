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
using CustomerManagement.WebApp.Filters;

namespace CustomerManagement.WebApp.Controllers
{
    [Exc]
    public class IncidentController : Controller
    {
        private IncidentManager incidentManager = new IncidentManager();
        private CategoryManager categoryManager = new CategoryManager();
        private CustomerManager customerManager = new CustomerManager();
        private CMUserManager cmUserManager = new CMUserManager();
        private ProductManager productManager = new ProductManager();

        public ActionResult Index()
        {
            var incidents = incidentManager.ListQueryable().Include(i => i.Category).Include(i => i.Customer).Include(i => i.Owner).Include(i => i.Product).ToList();
            return View(incidents.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident incident = incidentManager.Find(x => x.Id == id);
            if (incident == null)
            {
                return HttpNotFound();
            }
            return View(incident);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(customerManager.List(), "Id", "Fullname", "Surname");
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
            ViewBag.CustomerId = new SelectList(customerManager.List(), "Id", "Fullname", "Surname");
            ViewBag.OwnerId = new SelectList(cmUserManager.List(), "Id", "Fullname", "Surname");
            ViewBag.ProductId = new SelectList(productManager.List(), "Id", "Name");
            return View(incident);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident incident = incidentManager.Find(x => x.Id == id);
            if (incident == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(customerManager.List(), "Id", "Fullname", "Surname");
            ViewBag.OwnerId = new SelectList(cmUserManager.List(), "Id", "Fullname", "Surname");
            ViewBag.ProductId = new SelectList(productManager.List(), "Id", "Name", incident.ProductId);
            return View(incident);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Incident incident)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                //Note db_note = noteManager.Find(x => x.Id == note.Id);
                Incident inc = incidentManager.Find(x => x.Id == incident.Id);
                inc.CategoryId = incident.CategoryId;
                inc.CustomerId = incident.CustomerId;
                inc.Description = incident.Description;
                inc.EndDate = incident.EndDate;
                inc.OwnerId = incident.OwnerId;
                inc.ProductId = incident.ProductId;
                inc.StartDate = incident.StartDate;
                inc.StateCode = incident.StateCode;
                inc.Title = incident.Title;
                incidentManager.Update(inc);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(customerManager.List(), "Id", "Fullname", "Surname");
            ViewBag.OwnerId = new SelectList(cmUserManager.List(), "Id", "Fullname", "Surname");
            ViewBag.ProductId = new SelectList(productManager.List(), "Id", "Name");
            return View(incident);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident incident = incidentManager.Find(x => x.Id == id);
            if (incident == null)
            {
                return HttpNotFound();
            }
            return View(incident);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incident incident = incidentManager.Find(x => x.Id == id);
            incidentManager.Delete(incident);
            return RedirectToAction("Index");
        }
    }
}
