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
                name: "Home.AtivaUser",
                url: "ativarusuario/{hash}",
                defaults: new { controller = "Home", action = "AtivaUser" },
                namespaces: namespaces
            );



            //Rotas de Ocorrencias
            routes.MapRoute(
                name: "Home.Ocorrencia",
                url: "novaocorrencia",
                defaults: new { controller = "Ocorrencias", action = "Criar" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Home.Ocorrencia.CarregaLocal",
                url: "ocorrencias/carregaLocal",
                defaults: new { controller = "Ocorrencias", action = "CarregaLocal" },
                namespaces: namespaces
            );
            routes.MapRoute(
                name: "Home.Ocorrencia.CarregaItem",
                url: "ocorrencias/carregaitem",
                defaults: new { controller = "Ocorrencias", action = "CarregaItem" },
                namespaces: namespaces
            );
            routes.MapRoute(
               name: "Home.ConsultaChamado",
               url: "ocorrencias/consultachamado",
               defaults: new { controller = "Ocorrencias", action = "ConsultaChamado" },
               namespaces: namespaces
           );
            routes.MapRoute(
              name: "Home.Ocorrencia.CriarChamadoAPI",
              url: "ocorrencias/CriarChamadoAPI",
              defaults: new { controller = "Ocorrencias", action = "CriarChamadoAPI" },
              namespaces: namespaces
          );


        }
    }
}
