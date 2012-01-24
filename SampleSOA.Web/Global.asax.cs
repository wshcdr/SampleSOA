﻿using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using log4net.Config;

using MassTransit;

namespace SampleSOA.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                // Route name
                "{controller}/{action}/{id}",
                // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        public void ConfigureServiceBus()
        {
            Bus.Initialize(
                sbc =>
                    {
                        sbc.EnableRemoteIntrospection();
                        sbc.UseRabbitMqRouting();
                        sbc.ReceiveFrom("rabbitmq://localhost/sampleSoaWeb");
                    });
            Bus.Instance.WriteIntrospectionToFile(@"D:\POC\SampleSOA\logs\mt_web.log");
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure(new FileInfo("sampleSoaWeb.log4net.xml"));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ConfigureServiceBus();
        }
    }
}