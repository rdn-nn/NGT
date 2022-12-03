using NGT.Application;
using NGT.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;
using System.Drawing.Drawing2D;
using System.Web.UI.WebControls;

namespace NGT.Areas.Admin.Controllers
{
    public class OSController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Index()
        {

            ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias.Include(x=>x.StatusTicket).Where(x=>x.StatusTicket.Nome == "Pendente" || x.StatusTicket.Nome== "Em Andamento").OrderByDescending(x=>x.DataCriacao), "Id", "NumTicket");

            return View("OS_Lista");
        }

        [HttpPost]
        public ActionResult CarregaPatrimonio(List<int> OcoId)
        {
            if (OcoId != null)
            {
                //var teste = "";
                //if (OcoId.Count() > 1)
                //{
                //    for (var i = 0; i< OcoId.Count(); i++)
                //    {
                //        teste = teste + OcoId[i] +" || ";
                //    }
                //    teste = teste.Substring(0, teste.Length - 3);
                //} else
                //{
                    var teste = OcoId[0];
                //}

                var patrim = (from o in db.Ocorrencias.Include(x=>x.Item).Where(i => OcoId.Contains(i.Id))
                              select new
                              {
                                  id = o.ItemId,
                                  patrimonio = o.Item.Patrimonio,
                              }).ToArray();
                return Json(patrim);
            }
            return Json("");
        }
    }
}