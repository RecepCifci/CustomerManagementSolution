using CustomerManagement.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.WebApp.Filters
{
    public class AuthCustomer : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.Customer == null)
            {
                filterContext.Result = new RedirectResult("/Login/IndexCustomer");
            }
        }
    }
}