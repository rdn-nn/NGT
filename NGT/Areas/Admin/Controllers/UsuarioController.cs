using NGT.Application;
using NGT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGT.Areas.Admin.Controllers
{
    public class UsuarioController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Index()
        {
            return View();
        }
    }
}