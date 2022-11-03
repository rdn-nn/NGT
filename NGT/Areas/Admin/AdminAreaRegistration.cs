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
                name: "Admin.LogOut",
                url: "admin/sair",
                defaults: new { controller = "Dashboard", action = "LogOut" },
                namespaces: namespaces
            );
        }
    }
}