using NGT.Application;
using NGT.Data;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGT.Areas.Admin.Controllers
{
    public class LocalController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            ViewBag.locais = db.Locais.Include(b => b.Bloco).ToList().OrderBy(l=>l.Nome).OrderByDescending(b => b.StatusId);
            ViewBag.itens = db.Itens.Include(x => x.Local).ToList();
            return View("ListaLocal");
        }

        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.locais = db.Locais.Include(b => b.Bloco).Where(u => u.Nome.ToUpper().Contains(termo.ToUpper()) || u.Bloco.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            ViewBag.itens = db.Itens.Include(x => x.Local).ToList();
            return View("ListaLocal");
        }

        public ActionResult Novo()
        {
            ViewBag.BlocoId = new SelectList(db.Blocos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            return View("NovoLocal");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Novo(Local loc)
        {

            if (ModelState.IsValid)
            {
                Local local = new Local
                {
                    Nome = loc.Nome,
                    BlocoId = loc.BlocoId,
                    StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                };

                db.Locais.Add(local);
                db.SaveChanges();
                TempData["MSG"] = "success|Novo Local registrado com sucesso!|x";
                return RedirectToAction("Listar", "Local");
            }
            TempData["MSG"] = "error|Preencha todos os campos|x";
            return RedirectToAction("Listar", "Local");
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Local l = db.Locais.Find(Convert.ToInt32(id));

            if (l != null)
            {
                if (l.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    l.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    l.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(l).State = EntityState.Modified;
                db.SaveChanges();

                return Json(l.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
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
                Local l = db.Locais.Find(Convert.ToInt32(newId));

                if (l != null)
                {

                    if (id != "remove_" + l.Id)
                    {
                        if (l.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um local com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

                    db.Locais.Remove(l);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Local removido com sucesso!|x";
                    return Json(l != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir o local!|x";
                    return Json("f");
                }

            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível excluir o local, pois existem dados vinculados!|x";
                return Json("f");
            }
        }
        public ActionResult EditaLocal(string id)
        {
            Local l = db.Locais.Find(Convert.ToInt32(id));
            if (l != null)
            {
                ViewBag.local = l;
                if (l.Status.Nome == "Ativado")
                {
                    ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome");
                }
                else
                {
                    ViewBag.StatusId = new SelectList(db.Status.OrderBy(s => s.Id), "Id", "Nome");
                }

                ViewBag.BlocoId = new SelectList(db.Blocos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", l.BlocoId);

            }
            return View("EditLocal");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaLocal(Local loc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Local local = db.Locais.Find(loc.Id);

                    local.Nome = loc.Nome;
                    local.BlocoId = loc.BlocoId;
                    local.StatusId = loc.StatusId;

                    db.Entry(local).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MSG"] = "success|Local atualizado com sucesso";
                    return Listar();
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois locais com o mesmo nome!|x";
                return Listar();
            }
        }

    }
}