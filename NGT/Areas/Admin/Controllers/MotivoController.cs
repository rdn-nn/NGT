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
    public class MotivoController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            ViewBag.motivos = db.Motivos.ToList().OrderBy(c => c.Nome).OrderByDescending(c => c.StatusId);
            ViewBag.ocorrencias = db.Ocorrencias.Include(x => x.Motivo).ToList();
            return View("ListaMot");
        }
        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.motivos = db.Motivos.Where(u => u.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            ViewBag.ocorrencias = db.Ocorrencias.Include(x => x.Motivo).ToList();
            @TempData["LP"] = "x";
            return View("ListaMot");
        }
        public ActionResult Novo()
        {
            return View("NovoMot");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Novo(Motivo motiv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Motivo mot = new Motivo
                    {
                        Nome = motiv.Nome,
                        StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                    };

                    db.Motivos.Add(mot);
                    db.SaveChanges();
                    TempData["MSG"] = "success|Novo motivo registrado com sucesso!|x";
                    return Listar();
                }
                TempData["MSG"] = "error|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois motivos com o mesmo nome!|x";
                return Listar();
            }

        }
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Motivo m = db.Motivos.Find(Convert.ToInt32(id));

            if (m != null)
            {
                if (m.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    m.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    m.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();

                return Json(m.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
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
                Motivo m = db.Motivos.Find(Convert.ToInt32(newId));

                if (m != null)
                {

                    if (id != "remove_" + m.Id)
                    {
                        if (m.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um motivo com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

                    db.Motivos.Remove(m);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Motivo removido com sucesso!|x";
                    return Json(m != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir o motivo!|x";
                    return Json("f");
                }

            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível excluir este motivo, pois existem dados vinculados!|x";
                return Json("f");
            }
        }

        public ActionResult EditaMotivos(string id)
        {
            Motivo m = db.Motivos.Find(Convert.ToInt32(id));
            if (m != null)
            {
                ViewBag.Motivos = m;
                ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", m.StatusId);

            }
            return View("EditaMot");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaMotivos(Motivo Motiv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Motivo mot = db.Motivos.Find(Motiv.Id);

                    mot.Nome = Motiv.Nome;
                    mot.StatusId = Motiv.StatusId;

                    db.Entry(mot).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MSG"] = "success|Motivo atualizado com sucesso";
                    return Listar();
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois motivos com o mesmo nome!|x";
                return Listar();
            }
        }
    }
}