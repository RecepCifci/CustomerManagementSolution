using CustomerManagement.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.WebApp.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}