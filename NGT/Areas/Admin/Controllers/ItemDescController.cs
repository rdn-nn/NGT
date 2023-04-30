using NGT.Application;
using NGT.Data;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace NGT.Areas.Admin.Controllers
{
    public class ItemDescController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Novo(string id)
        {
            Item i = db.Itens.Find(Convert.ToInt32(id));
            if (i != null)
            {
                ViewBag.ItemId = new SelectList(db.Itens.Where(x => x.Id == i.Id), "Id", "Nome");
                ViewBag.LocalId = new SelectList(db.Locais.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
                ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
                return View("Novo");
            }
            return RedirectToAction("Listar", "Item");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Novo(ItemDesc itd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemDesc ite = new ItemDesc
                    {
                        ItemId = itd.ItemId,
                        NumSerie = itd.NumSerie,
                        Patrimonio = itd.Patrimonio,
                        HasPlaca = itd.HasPlaca,
                        IsPatInterno = itd.IsPatInterno,
                        LocalId = itd.LocalId,
                        CategoriaId = itd.CategoriaId,
                        StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                    };

                    db.ItemDescs.Add(ite);
                    db.SaveChanges();
                    TempData["MSG"] = "success|Novo item registrado com sucesso!|x";
                    return RedirectToAction("Listar", "Item");
                }
                TempData["MSG"] = "error|Preencha todos os campos|x";
                return RedirectToAction("Listar", "Item");
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois itens com o mesmo nome!|x";
                return RedirectToAction("Listar", "Item");
            }
        }

        public ActionResult Edita(string id)
        {
            ItemDesc itd = db.ItemDescs.Find(Convert.ToInt32(id));
            if (itd != null)
            {
                ViewBag.ItemDesc = itd;
                ViewBag.ItemId = new SelectList(db.Itens.Where(x => x.Id == itd.ItemId), "Id", "Nome", itd.ItemId);
                ViewBag.LocalId = new SelectList(db.Locais.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", itd.LocalId);
                ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", itd.CategoriaId);
                ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", itd.StatusId);
                return View("EditaItemDesc");

            }
            return RedirectToAction("Listar", "Item");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edita(ItemDesc item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemDesc ite = db.ItemDescs.Find(item.Id);

                    ite.ItemId = item.ItemId;
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
                    return RedirectToAction("Listar", "Item");
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return RedirectToAction("Listar", "Item");
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois itens com o mesmo nome!|x";
                return RedirectToAction("Listar", "Item");
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
                ItemDesc i = db.ItemDescs.Find(Convert.ToInt32(newId));

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

                    db.ItemDescs.Remove(i);
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
    }
}