using CustomerManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Entities.ModelView;
using CustomerManagement.BusinessLayer;
using CustomerManagement.BusinessLayer.Results;
using CustomerManagement.WebApp.Models;
using CustomerManagement.WebApp.Filters;

namespace CustomerManagement.WebApp.Controllers
{
    [Exc]
    public class LoginController : Controller
    {
        private LoginManager loginManager = new LoginManager();
        private LoginCustomerManager loginCustomerManager = new LoginCustomerManager();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<CMUser> res = loginManager.LoginUser(loginModel);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(loginModel);
                }
                CurrentSession.Set("login", res.Result);
                return RedirectToAction("Index", "Home");
            }
            return View(loginModel);
        }

        public ActionResult IndexCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndexCustomer(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Customer> res = loginCustomerManager.LoginCustomer(loginModel);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(loginModel);
                }
                CurrentSession.Set("logincustomer", res.Result);
                return RedirectToAction("Index", "Incident", new { area = "CustomerArea" });
            }
            return View(loginModel);
        }
    }
}