﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectOnlineStore
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            DbInitializer.SeedAdmin();
            DbInitializer.SeedProducts();
            DbInitializer.SeedBlogs();

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}