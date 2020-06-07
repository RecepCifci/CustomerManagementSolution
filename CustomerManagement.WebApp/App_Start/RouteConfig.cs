using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CustomerManagement.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                             "Default",
                             "{controller}/{action}/{id}",
                             new { controller = "Login", action = "IndexCustomer", id = UrlParameter.Optional },
                             new[] { "CustomerManagement.WebApp.Controllers" }
            // new[] { "CustomerManagement.WebApp.Areas.CustomerArea.Controllers" }  // --> Area içindeki Controller ı açar
            //new[] { "CustomerManagement.WebApp.Controllers" } // --> Projedeki Area da bulunmayan Controller ı açar
            // Yukarıdaki gibi yazılırsa Area dışındaki controller lara gider
            );
        }
    }
}
