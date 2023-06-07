using NGT.Application;
using NGT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;

namespace NGT.Areas.Admin.Controllers
{
    public class ManutencaoTipoController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        // GET: Admin/ManutencaoTipo
        public ActionResult Listar()
        {
            ViewBag.tipos = db.ManutencaoTipo.ToList().OrderBy(l => l.Nome).OrderByDescending(b => b.StatusId);
            ViewBag.os = db.OrdServicos.Include(x => x.ManutencaoTipo).ToList();
            return View("ListaManutTipos");
        }

        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.tipos = db.ManutencaoTipo.Where(u => u.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            ViewBag.os = db.OrdServicos.Include(x => x.ManutencaoTipo).ToList();
            @TempData["LP"] = "x";
            return View("ListaManutTipos");
        }
        public ActionResult Novo()
        {
            return View("NovoManutTipos");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Novo(ManutencaoTipo tipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ManutencaoTipo manut = new ManutencaoTipo
                    {
                        Nome = tipo.Nome,
                        StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                    };

                    db.ManutencaoTipo.Add(manut);
                    db.SaveChanges();
                    TempData["MSG"] = "success|Novo tipo registrado com sucesso!|x";
                    return Listar();
                }
                TempData["MSG"] = "error|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois tipos com o mesmo nome!|x";
                return Listar();
            }

        }

        public ActionResult EditaTipos(string id)
        {
            ManutencaoTipo tp = db.ManutencaoTipo.Find(Convert.ToInt32(id));
            if (tp != null)
            {
                ViewBag.tipos = tp;
                ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", tp.StatusId);

            }
            return View("EditaManutTipos");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaTipos(ManutencaoTipo tipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ManutencaoTipo tp = db.ManutencaoTipo.Find(tipo.Id);

                    tp.Nome = tipo.Nome;
                    tp.StatusId = tipo.StatusId;

                    db.Entry(tp).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MSG"] = "success|Tipo atualizado com sucesso";
                    return Listar();
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois tipos com o mesmo nome!|x";
                return Listar();
            }
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            ManutencaoTipo tp = db.ManutencaoTipo.Find(Convert.ToInt32(id));

            if (tp != null)
            {
                if (tp.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    tp.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    tp.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(tp).State = EntityState.Modified;
                db.SaveChanges();

                return Json(tp.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
            }
            else
            {
                return Json("n");
            }
        }
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult Remover(string id)
        {
            try
            {
                string newId = "";
                if (id.Contains("_"))
                {
                    newId = id.Substring(7);
                }
                else
                {
                    newId = id;
                }
                ManutencaoTipo tp = db.ManutencaoTipo.Find(Convert.ToInt32(newId));

                if (tp != null)
                {

                    if (id != "remove_" + tp.Id)
                    {
                        if (tp.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um tipo com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

                    db.ManutencaoTipo.Remove(tp);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Tipo removido com sucesso!|x";
                    return Json(tp != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir o tipo!|x";
                    return Json("f");
                }

            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível excluir este tipo, pois existem dados vinculados!|x";
                return Json("f");
            }
        }
    }
}