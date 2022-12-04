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
                name: "Admin.Usuario.EditaUser",
                url: "admin/usuarios/editausuario/{id}",
                defaults: new { controller = "Usuario", action = "EditaUser" },
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
                name: "Admin.Fornecedor.Index",
                url: "admin/fornecedores",
                defaults: new { controller = "Fornecedor", action = "Listar" },
                namespaces: namespaces
            );
            context.MapRoute(
               name: "Admin.Fornecedor.Filtrar",
               url: "admin/fornecedores/filtrar",
               defaults: new { controller = "Fornecedor", action = "ListarFiltrado" },
               namespaces: namespaces
           );
            context.MapRoute(
                name: "Admin.Fornecedor.TrocarStatus",
                url: "admin/fornecedores/trocarstatus",
                defaults: new { controller = "Fornecedor", action = "TrocarStatus" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Fornecedor.NovoFornecedor",
                url: "admin/fornecedores/cadastrafornecedor",
                defaults: new { controller = "Fornecedor", action = "NovoFornecedor" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Fornecedor.RemoveFornecedor",
                url: "admin/fornecedores/deletafornecedor",
                defaults: new { controller = "Fornecedor", action = "RemoveFornecedor" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Fornecedor.EditaFornecedor",
                url: "admin/fornecedores/editafornecedor/{id}",
                defaults: new { controller = "Fornecedor", action = "EditaFornecedor" },
                namespaces: namespaces
            );
            context.MapRoute(
                name: "Admin.Fornecedor.ExibeFornecedor",
                url: "admin/fornecedores/exibefornecedor/{id}",
                defaults: new { controller = "Fornecedor", action = "ExibeFornecedor" },
                namespaces: namespaces
            );

            context.MapRoute(
                name: "Admin.LogOut",
                url: "admin/sair",
                defaults: new { controller = "Dashboard", action = "LogOut" },
                namespaces: namespaces
            );








            //LOCAL
            context.MapRoute(
               name: "Admin.Local.Index",
               url: "admin/locais",
               defaults: new { controller = "Local", action = "Listar" },
               namespaces: namespaces
           );
            context.MapRoute(
                name: "Admin.Local.TrocarStatus",
                url: "admin/locais/trocarstatus",
                defaults: new { controller = "Local", action = "TrocarStatus" },
                namespaces: namespaces
            );
            context.MapRoute(
               name: "Admin.Local.Novo",
               url: "admin/locais/novo-local",
               defaults: new { controller = "Local", action = "Novo" },
               namespaces: namespaces
           );
            context.MapRoute(
               name: "Admin.Local.Remover",
               url: "admin/locais/remove-local",
               defaults: new { controller = "Local", action = "Remover" },
               namespaces: namespaces
           );
            context.MapRoute(
               name: "Admin.Local.Filtrar",
               url: "admin/locais/filtrar",
               defaults: new { controller = "Local", action = "ListarFiltrado" },
               namespaces: namespaces
           );
            context.MapRoute(
                name: "Admin.Local.EditaLocais",
                url: "admin/locais/editalocais/{id}",
                defaults: new { controller = "Local", action = "EditaLocal" },
                namespaces: namespaces
            );



            //CATEGORIA
            context.MapRoute(
               name: "Admin.Categoria.Index",
               url: "admin/categorias",
               defaults: new { controller = "Categoria", action = "Listar" },
               namespaces: namespaces
           );
            context.MapRoute(
                name: "Admin.Categoria.TrocarStatus",
                url: "admin/categorias/trocarstatus",
                defaults: new { controller = "Categoria", action = "TrocarStatus" },
                namespaces: namespaces
            );
            context.MapRoute(
               name: "Admin.Categoria.Novo",
               url: "admin/categorias/nova-categoria",
               defaults: new { controller = "Categoria", action = "Novo" },
               namespaces: namespaces
           );
            context.MapRoute(
               name: "Admin.Categoria.Remover",
               url: "admin/categorias/remove-categoria",
               defaults: new { controller = "Categoria", action = "Remover" },
               namespaces: namespaces
           );
            context.MapRoute(
               name: "Admin.Categoria.Filtrar",
               url: "admin/categorias/filtrar",
               defaults: new { controller = "Categoria", action = "ListarFiltrado" },
               namespaces: namespaces
           );
            context.MapRoute(
                name: "Admin.Categoria.EditaCategoria",
                url: "admin/categorias/editacategorias/{id}",
                defaults: new { controller = "Categoria", action = "EditaCategoria" },
                namespaces: namespaces
            );
        }
    }
}