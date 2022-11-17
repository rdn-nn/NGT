﻿using NGT.Application;
using NGT.Data;
using NGT.Models.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace NGT.Areas.Admin.Controllers
{
    public class BlocoController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            ViewBag.blocos = db.Blocos.ToList().OrderByDescending(b => b.StatusId);
            return View("Lista");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Criar(Bloco blo)
        {

            if (ModelState.IsValid)
            {
                Bloco bloco = new Bloco
                {
                    Nome = blo.Nome,
                    StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id
                };

                db.Blocos.Add(bloco);
                db.SaveChanges();
                TempData["MSG"] = "success|Novo Bloco registrado com sucesso!|x";
                return RedirectToAction("Listar", "Bloco");
            }
            TempData["MSG"] = "error|Preencha todos os campos|x";
            return RedirectToAction("Listar", "Bloco");
        }


        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Bloco b = db.Blocos.Find(Convert.ToInt32(id));

            if (b != null)
            {
                if (b.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    b.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    b.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();

                return Json(b.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
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
                Bloco b = db.Blocos.Find(Convert.ToInt32(id));

                if (b != null)
                {

                    db.Blocos.Remove(b);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Bloco removido com sucesso!|x";
                    return Json(b != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Impossível excluir o bloco, pois existem dados vinculados!|x";
                    return Json("f");
                }
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível excluir o bloco, pois existem dados vinculados!|x";
                return Json("f");
            }
        }
    }
}