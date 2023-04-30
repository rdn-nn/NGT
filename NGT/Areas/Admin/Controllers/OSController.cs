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

            //ViewBag.OcorrenciaId = new SelectList(db.Ocorrencias.Include(x=>x.StatusTicket).Where(x=>x.StatusTicket.Nome == "Pendente" || x.StatusTicket.Nome== "Em Andamento").OrderByDescending(x=>x.DataCriacao), "Id", "NumTicket");
            ViewBag.BlocoId = new SelectList(db.Blocos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            //LocalId = new SelectList(db.Locais.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            //ViewBag.ItemId = new SelectList(db.Itens.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.MotivoId = new SelectList(db.Motivos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.FornecedorId = new SelectList(db.Fornecedores.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "NomeFantasia");
            return View("OS_Lista");
        }

        //[HttpPost]
        //public ActionResult CarregaPatrimonio(List<int> OcoId)
        //{
        //    if (OcoId != null)
        //    {
        //        //var teste = "";
        //        //if (OcoId.Count() > 1)
        //        //{
        //        //    for (var i = 0; i< OcoId.Count(); i++)
        //        //    {
        //        //        teste = teste + OcoId[i] +" || ";
        //        //    }
        //        //    teste = teste.Substring(0, teste.Length - 3);
        //        //} else
        //        //{
        //            var teste = OcoId[0];
        //        //}

        //        var patrim = (from o in db.Ocorrencias.Include(x=>x.Item).Where(i => OcoId.Contains(i.Id))
        //                      select new
        //                      {
        //                          id = o.ItemId,
        //                          patrimonio = o.Item.Patrimonio,
        //                      }).ToArray();
        //        return Json(patrim);
        //    }
        //    return Json("");
        //}


        //[HttpPost]
        //public ActionResult CarregaLocal(int blocoId)
        //{
        //    var locais = (from l in db.Locais.Include(x => x.Bloco).Include(x => x.Status)
        //                  where l.Bloco.Id == blocoId && l.Status.Nome == "Ativado"
        //                  select new
        //                  {
        //                      id = l.Id,
        //                      nome = l.Nome
        //                  }).ToArray();
        //    return Json(locais);
        //}
        //[HttpPost]
        //public ActionResult CarregaItem(int localId, int categId)
        //{
        //    var itens = (from i in db.ItemDescs.Include(x => x.Item).Include(x => x.Local).Include(x => x.Categoria).Include(x => x.Status)
        //                 where i.Local.Id == localId && i.CategoriaId == categId && i.Status.Nome == "Ativado"
        //                 select new
        //                 {
        //                     id = i.Item.Id,
        //                     nome = i.Item.Nome,
        //                 }).Distinct().ToArray();
        //    return Json(itens);
        //}

k

    }
}