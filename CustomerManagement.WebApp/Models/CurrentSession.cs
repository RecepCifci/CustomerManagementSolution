﻿using CustomerManagement.Entities;
using CustomerManagement.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagement.WebApp.Models
{
    public class CurrentSession
    {
        public static CMUser User
        {
            get
            {
                return Get<CMUser>("login");
            }
        }
        public static Customer Customer
        {
            get
            {
                return Get<Customer>("logincustomer");
            }
        }
        public static void Set<T>(string key, T obj)
        {
            HttpContext.Current.Session[key] = obj;
        }

        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }

            return default(T);
        }

        public static void Remove(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}