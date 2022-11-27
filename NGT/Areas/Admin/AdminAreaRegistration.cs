using System.Web.Mvc;

namespace NGT.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            string[] namespaces = new string[] {
                "NGT.Areas.Admin.Controllers"
            };
            context.MapRoute(
                name: "Admin.Dashboard.Index",
                url: "admin/dashboard",
                defaults: new { controller = "Dashboard", action = "Index" },
                namespaces: namespaces
            );



            context.MapRoute(
                name: "Admin.Usuario.Index",
                url: "admin/usuarios",
                defaults: new { controller = "Usuario", action = "Listar" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Usuario.Filtrar",
                url: "admin/usuarios/filtrar",
                defaults: new { controller = "Usuario", action = "ListarFiltrado" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Usuario.TrocarStatus",
                url: "admin/usuarios/trocarstatus",
                defaults: new { controller = "Usuario", action = "TrocarStatus" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Usuario.NovoUser",
                url: "admin/usuarios/cadastrausuario",
                defaults: new { controller = "Usuario", action = "NovoUser" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Usuario.RemoveUser",
                url: "admin/usuarios/deletausuario",
                defaults: new { controller = "Usuario", action = "RemoveUser" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Usuario.DetalhesUser",
                url: "admin/usuarios/detalhesusuario",
                defaults: new { controller = "Usuario", action = "DetalhesUser" },
                namespaces: namespaces
            );


            context.MapRoute(
                name: "Admin.Ocorrencia.Index",
                url: "admin/ocorrencias",
                defaults: new { controller = "Ocorrencia", action = "Index" },
                namespaces: namespaces
            );
            context.MapRoute(
               name: "Admin.Ocorrencia",
               url: "admin/ocorrencias/nova-ocorrencia",
               defaults: new { controller = "Ocorrencia", action = "Criar" },
               namespaces: namespaces
           );
            context.MapRoute(
               name: "Admin.Ocorrencia.CarregaLocal",
               url: "admin/ocorrencias/carregaLocal",
               defaults: new { controller = "Ocorrencia", action = "CarregaLocal" },
               namespaces: namespaces
           );
            context.MapRoute(
                name: "Admin.Ocorrencia.CarregaItem",
                url: "admin/ocorrencias/carregaitem",
                defaults: new { controller = "Ocorrencia", action = "CarregaItem" },
                namespaces: namespaces
            );
            //context.MapRoute(
            //    name: "Admin.Ocorrencia.AlteraStatus",
            //    url: "admin/ocorrencias/AlteraStatus/{ticket}",
            //    defaults: new { controller = "Ocorrencia", action = "AlteraStatus" },
            //    namespaces: namespaces
            //);





            context.MapRoute(
                name: "Admin.Bloco.Index",
                url: "admin/blocos",
                defaults: new { controller = "Bloco", action = "Listar" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Bloco.TrocarStatus",
                url: "admin/blocos/trocarstatus",
                defaults: new { controller = "Bloco", action = "TrocarStatus" },
                namespaces: namespaces
            );
            context.MapRoute(
               name: "Admin.Bloco",
               url: "admin/blocos/novo-bloco",
               defaults: new { controller = "Bloco", action = "Criar" },
               namespaces: namespaces
           );
            context.MapRoute(
               name: "Admin.Bloco.Remover",
               url: "admin/blocos/remove-bloco",
               defaults: new { controller = "Bloco", action = "Remover" },
               namespaces: namespaces
           );



            context.MapRoute(
                name: "Admin.OS.Index",
                url: "admin/os",
                defaults: new { controller = "OS", action = "Index" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.OS.CarregarPatrimonio",
                url: "admin/os/carregapatrimonio",
                defaults: new { controller = "OS", action = "CarregaPatrimonio" },
                namespaces: namespaces
            );



            context.MapRoute(
                name: "Admin.LogOut",
                url: "admin/sair",
                defaults: new { controller = "Dashboard", action = "LogOut" },
                namespaces: namespaces
            );
        }
    }
}