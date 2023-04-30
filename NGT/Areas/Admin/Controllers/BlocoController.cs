using NGT.Application;
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
            ViewBag.locais = db.Locais.Include(x=>x.Bloco).ToList();
            ViewBag.itens = db.ItemDescs.Include(x => x.Local).ToList();
            //ViewBag.itens = db.Itens.Include(x=>x.ItemDescs).ToList();
            

            //ViewBag.itens = db.Itens.ToList();

            //var locais = db.Locais.Include(i=>i.Item);

            //foreach (Local l in locais)
            //{
            //    foreach (Item i in l.Item)
            //    {
            //        ViewBag.TotalList = l.BlocoId + i.Id;
            //    }
            //}
            return View("Lista");
        }

        public ActionResult Criar()
        {
            return View("NovoBloco");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Criar(Bloco blo)
        {
            try
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
            catch (Exception)
            {
                @TempData["MSG"] = "error|Erro ao gravar novo Bloco. O nome indicado já existe!|x";
                return RedirectToAction("Listar", "Bloco");
            }
        }

        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }

            //ViewBag.locais = db.Locais.Include(b => b.Bloco).Where(u => u.Nome.ToUpper().Contains(termo.ToUpper()) || u.Bloco.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            //ViewBag.itens = db.Itens.Include(x => x.Local).ToList();

            ViewBag.blocos = db.Blocos.Where(u => u.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            ViewBag.locais = db.Locais.Include(x => x.Bloco).ToList();
            ViewBag.itens = db.ItemDescs.Include(x => x.Local).ToList();
            @TempData["LP"] = "x";
            return View("Lista");
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
                string newId = "";
                if (id.Contains("_"))
                {
                    newId = id.Substring(7);
                }
                else
                {
                    newId = id;
                }

                Bloco b = db.Blocos.Find(Convert.ToInt32(newId));

                if (b != null)
                {
                    if (id != "remove_" + b.Id)
                    {
                        if (b.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um bloco com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

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

        public ActionResult Edita(string id)
        {
            Bloco b = db.Blocos.Find(Convert.ToInt32(id));
            if (b != null)
            {
                ViewBag.bloco = b;
                if (b.Status.Nome == "Ativado")
                {
                    ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome");
                }
                else
                {
                    ViewBag.StatusId = new SelectList(db.Status.OrderBy(s => s.Id), "Id", "Nome");
                }

            }
            return View("EditaBloco");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edita(Bloco blo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bloco bloco = db.Blocos.Find(blo.Id);

                    bloco.Nome = blo.Nome;
                    bloco.StatusId = blo.StatusId;

                    db.Entry(bloco).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MSG"] = "success|Bloco atualizado com sucesso";
                    return Listar();
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return Listar();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Impossível registrar dois blocos com o mesmo nome!|x";
                return Listar();
            }
        }
    }
}