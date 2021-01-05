﻿using System.Globalization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using ToDoApp.Business;

namespace ToDoApp.WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer _container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            _container = new UnityContainer();
            FtUnityMapper.RegisterComponents(_container);
        }

        protected void Application_BeginRequest()
        {
            var cInf = new CultureInfo("en-US", false)
            {
                DateTimeFormat =
                {
                    DateSeparator = "/",
                    ShortDatePattern = "dd/MM/yyyy",
                    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
                }
            };
            // NOTE: change the culture name en-ZA to whatever culture suits your needs

            System.Threading.Thread.CurrentThread.CurrentCulture = cInf;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cInf;

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");


            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization, Accept"); HttpContext.Current.Response.End();
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS,PUT,DELETE");
                HttpContext.Current.Response.End();

            }

        }
    }
}
