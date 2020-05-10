﻿using System.Web;
using System.Web.Mvc;
using TransactionApp.Filters;

namespace TransactionApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}