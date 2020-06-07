using System.Web.Mvc;

namespace CustomerManagement.WebApp.Areas.CustomerArea
{
    public class CustomerAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CustomerArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "CustomerArea_default",
            //    "CustomerArea/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                              "CustomerArea_default",
                              "CustomerArea/{controller}/{action}/{id}",
                              new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                              new[] { "CustomerManagement.WebApp.Areas.CustomerArea.Controllers" }
);
        }
    }
}