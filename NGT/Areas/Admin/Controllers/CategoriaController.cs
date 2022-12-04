using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using NGT.Data;
using NGT.Models.Entities;

namespace NGT.Areas.Admin.Controllers
{
    public class CategoriaController : Controller
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            ViewBag.categorias = db.Categorias.ToList().OrderBy(c => c.Nome).OrderByDescending(c => c.StatusId);
            ViewBag.itens = db.Itens.Include(x => x.Categoria).ToList();
            return View("ListaCat");
        }
        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.categorias = db.Categorias.Where(u => u.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            ViewBag.itens = db.Itens.Include(x => x.Categoria).ToList();
            return View("ListaCat");
        }
        public ActionResult Novo()
        {
            return View("NovaCateg");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Novo(Categoria categ)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Categoria cat = new Categoria
                    {
                        Nome = categ.Nome,
                        StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                    };

                    db.Categorias.Add(cat);
                    db.SaveChanges();
                    TempData["MSG"] = "success|Nova categoria registrada com sucesso!|x";
                    return Listar();
                }
                TempData["MSG"] = "error|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar duas categorias com o mesmo nome!|x";
                return Listar();
            }

        }
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Categoria c = db.Categorias.Find(Convert.ToInt32(id));

            if (c != null)
            {
                if (c.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    c.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    c.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();

                return Json(c.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
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
                Categoria c = db.Categorias.Find(Convert.ToInt32(newId));

                if (c != null)
                {

                    if (id != "remove_" + c.Id)
                    {
                        if (c.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir uma categoria com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

                    db.Categorias.Remove(c);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Categoria removida com sucesso!|x";
                    return Json(c != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir a categoria!|x";
                    return Json("f");
                }

            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível excluir esta categoria, pois existem dados vinculados!|x";
                return Json("f");
            }
        }

        public ActionResult EditaCategoria(string id)
        {
            Categoria c = db.Categorias.Find(Convert.ToInt32(id));
            if (c != null)
            {
                ViewBag.Categoria = c;
                ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", c.StatusId);

            }
            return View("EditaCategoria");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaCategoria(Categoria cat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Categoria categ = db.Categorias.Find(cat.Id);

                    categ.Nome = cat.Nome;
                    categ.StatusId = cat.StatusId;

                    db.Entry(categ).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MSG"] = "success|Categoria atualizada com sucesso";
                    return Listar();
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar duas categorias com o mesmo nome!|x";
                return Listar();
            }
        }
    }
}
