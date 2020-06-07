using System;
using CustomerManagement.Common;
using System.Web;
using CustomerManagement.Entities;
using CustomerManagement.Entities.Base;

namespace CustomerManagement.WebApp
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            if (HttpContext.Current.Session["login"] != null)
            {
                CMUser user = HttpContext.Current.Session["login"] as CMUser;
                return user.Fullname;
            }
            return "system";
        }
    }
}