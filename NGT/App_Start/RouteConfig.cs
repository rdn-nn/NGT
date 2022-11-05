using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NGT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string[] namespaces = new string[] {
                " NGT.Controllers"
            };

            routes.MapRoute(
                name: "Home.Index",
                url: "",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: namespaces
            );

            routes.MapRoute(
                name: "Home.Acesso",
                url: "acesso",
                defaults: new { controller = "Home", action = "Acesso" },
                namespaces: namespaces
            );

            routes.MapRoute(
                name: "Home.Ocorrencia",
                url: "nova-ocorrencia",
                defaults: new { controller = "Ocorrencia", action = "Criar" },
                namespaces: namespaces
            );
        }
    }
}
