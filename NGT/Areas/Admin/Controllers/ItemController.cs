using ImageResizer.Configuration.Xml;
using NGT.Application;
using NGT.Data;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NGT.Areas.Admin.Controllers
{
    public class ItemController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            ViewBag.itens = db.Itens.ToList().OrderBy(c => c.Nome).OrderByDescending(c => c.StatusId);
            ViewBag.ocorrencias = db.Ocorrencias.Include(x => x.Item).ToList();
            return View("ListaItem");
        }
        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.itens = db.Itens.Where(u => u.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            ViewBag.ocorrencias = db.Ocorrencias.Include(x => x.Item).ToList();
            @TempData["LP"] = "x";
            return View("ListaItem");
        }
        public ActionResult Novo()
        {
            ViewBag.LocalId = new SelectList(db.Locais.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");

            return View("NovoItem");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Novo(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Item ite = new Item
                    {
                        Nome = item.Nome,
                        NumSerie = item.NumSerie,
                        Patrimonio = item.Patrimonio,
                        HasPlaca = item.HasPlaca,
                        IsPatInterno = item.IsPatInterno,
                        LocalId = item.LocalId,
                        CategoriaId = item.CategoriaId,
                        StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                    };

                    db.Itens.Add(ite);
                    db.SaveChanges();
                    TempData["MSG"] = "success|Novo item registrado com sucesso!|x";
                    return Listar();
                }
                TempData["MSG"] = "error|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois itens com o mesmo nome!|x";
                return Listar();
            }

        }
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Item i = db.Itens.Find(Convert.ToInt32(id));

            if (i != null)
            {
                if (i.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    i.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    i.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(i).State = EntityState.Modified;
                db.SaveChanges();

                return Json(i.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
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
                Item i = db.Itens.Find(Convert.ToInt32(newId));

                if (i != null)
                {

                    if (id != "remove_" + i.Id)
                    {
                        if (i.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um item com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

                    db.Itens.Remove(i);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Item removido com sucesso!|x";
                    return Json(i != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir este item!|x";
                    return Json("f");
                }

            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível excluir este item, pois existem dados vinculados!|x";
                return Json("f");
            }
        }

        public ActionResult EditaItem(string id)
        {
            Item i = db.Itens.Find(Convert.ToInt32(id));
            if (i != null)
            {
                ViewBag.Itens = i;
                ViewBag.LocalId = new SelectList(db.Locais.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", i.LocalId);
                ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", i.CategoriaId);
                ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", i.StatusId);

            }
            return View("EditaItem");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaItem(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Item ite = db.Itens.Find(item.Id);

                    ite.Nome = item.Nome;
                    ite.NumSerie = item.NumSerie;
                    ite.Patrimonio = item.Patrimonio;
                    ite.HasPlaca = item.HasPlaca;
                    ite.IsPatInterno = item.IsPatInterno;
                    ite.LocalId = item.LocalId;
                    ite.CategoriaId = item.CategoriaId;
                    ite.StatusId = item.StatusId;

                    db.Entry(ite).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MSG"] = "success|Item atualizado com sucesso";
                    return Listar();
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois itens com o mesmo nome!|x";
                return Listar();
            }
        }
        public ActionResult ExibeItem(string id)
        {
            Item i = db.Itens.Find(Convert.ToInt32(id));
            if (i != null)
            {
                ViewBag.Itens = i;
                //ViewBag.LocalId = new SelectList(db.Locais.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", i.LocalId);
                //ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", i.CategoriaId);
                //ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", i.StatusId);

            }
            return View("ShowItem");
        }
    }
}