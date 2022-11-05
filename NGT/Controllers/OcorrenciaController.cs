using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGT.Controllers
{
    public class OcorrenciaController : Controller
    {
        // GET: Ocorrencia
        public ActionResult Criar()
        {
            return View("NovaOcorrencia");
        }
    }
}