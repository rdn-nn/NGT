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
    public class FornecedorController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            ViewBag.fornecedores = db.Fornecedores.ToList().OrderBy(f => f.NomeFantasia);
            return View("Lista");
        }

        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.fornecedores = db.Fornecedores.Where(u => u.NomeFantasia.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.NomeFantasia);
            return View("Lista");
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Fornecedor f = db.Fornecedores.Find(Convert.ToInt32(id));

            if (f != null)
            {
                
                if (f.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    f.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    f.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();

                return Json(f.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
            }
            else
            {
                return Json("n");
            }
        }

        public ActionResult NovoFornecedor()
        {
            return View("NovoFornec");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NovoFornecedor(Fornecedor forn)
        {
            forn.StatusId = 1;
            if (ModelState.IsValid)
            {
                Fornecedor fornec = new Fornecedor
                {
                    RazaoSoc = forn.RazaoSoc,
                    NomeFantasia = forn.NomeFantasia,
                    CNPJ = forn.CNPJ,
                    IE = forn.IE,
                    Endereco = forn.Endereco,
                    Telefone = forn.Telefone,
                    Email = forn.Email,
                    ServPrestado = forn.ServPrestado,
                    NomeResp = forn.NomeResp,
                    CargoResp = forn.CargoResp,
                    RamoAtiv = forn.RamoAtiv,
                    Obs = forn.Obs,
                    StatusId = forn.StatusId
                };

                db.Fornecedores.Add(fornec);
                db.SaveChanges();
                TempData["MSG"] = "success|Fornecedor cadastrado com sucesso";
                return Listar();
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return Listar();
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult RemoveFornecedor(string id)
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


                Fornecedor f = db.Fornecedores.Find(Convert.ToInt32(newId));

                if (f != null)
                {

                    if (id != "remove_" + f.Id)
                    {
                        if (f.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um fornecedor com status ATIVADO!|x";
                            return Json("f");
                        }
                    }

                    db.Fornecedores.Remove(f);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Fornecedor removido com sucesso!|x";
                    return Json(f != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir o fornecedor!|x";
                    return Json("f");
                }
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Erro ao excluir o fornecedor!|x";
                return Json("f");
            }
        }

        public ActionResult EditaFornecedor(string id)
        {
            Fornecedor f = db.Fornecedores.Find(Convert.ToInt32(id));
            if (f != null)
            {
                ViewBag.Fornecedor = f;
                if (f.Status.Nome == "Ativado")
                {

                    ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome");
                }
                else
                {
                    ViewBag.StatusId = new SelectList(db.Status.OrderBy(s => s.Id), "Id", "Nome");
                }
            }
            return View("EditFornec");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaFornecedor(Fornecedor forn)
        {
            if (ModelState.IsValid)
            {
                Fornecedor fornec = db.Fornecedores.Find(forn.Id);

                fornec.RazaoSoc = forn.RazaoSoc;
                fornec.NomeFantasia = forn.NomeFantasia;
                fornec.CNPJ = forn.CNPJ;
                fornec.IE = forn.IE;
                fornec.Endereco = forn.Endereco;
                fornec.Telefone = forn.Telefone;
                fornec.Email = forn.Email;
                fornec.ServPrestado = forn.ServPrestado;
                fornec.NomeResp = forn.NomeResp;
                fornec.CargoResp = forn.CargoResp;
                fornec.RamoAtiv = forn.RamoAtiv;
                fornec.Obs = forn.Obs;
                fornec.StatusId = forn.StatusId;

                db.Entry(fornec).State = EntityState.Modified;
                db.SaveChanges();

                TempData["MSG"] = "success|Fornecedor atualizado com sucesso";
                return Listar();
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return Listar();
        }

        public ActionResult ExibeFornecedor(string id)
        {
            Fornecedor f = db.Fornecedores.Find(Convert.ToInt32(id));
            if (f != null)
            {
                ViewBag.Fornecedor = f;
                return View("ShowFornec");
            }
            @TempData["MSG"] = "error|Não foi possível encontrar o cadastro do fornecedor indicado!|x";
            return View("ShowFornec");
        }
    }
}