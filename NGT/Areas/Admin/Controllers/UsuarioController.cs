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
    public class UsuarioController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {
            
            ViewBag.usuarios = db.Usuarios.ToList().OrderBy(u => u.Nome);
            return View("ListaUser");
        }

        public ActionResult ListarFiltrado(string termo)
        {
            if (string.IsNullOrEmpty(termo))
            {
                @TempData["MSG"] = "warning|Nenhuma busca realizada. Preencha o campo de busca corretamente!|x";
                return Listar();
            }
            ViewBag.usuarios = db.Usuarios.Where(u => u.Nome.ToUpper().Contains(termo.ToUpper())).ToList().OrderBy(u => u.Nome);
            return View("ListaUser");
        }

        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult TrocarStatus(string id)
        {
            Usuario u = db.Usuarios.Find(Convert.ToInt32(id));

            if (u != null)
            {
                if (u.Id == Convert.ToInt32(User.Identity.Name.Split('|')[0]))
                {
                    @TempData["MSG"] = "error|Não é possível desativar seu próprio usuário!|x";
                    return Json("n");
                }

                if (u.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id)
                {
                    u.StatusId = db.Status.Where(x => x.Nome == "Desativado").FirstOrDefault().Id;
                }
                else
                {
                    u.StatusId = db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id;
                }

                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();

                return Json(u.StatusId == db.Status.Where(x => x.Nome == "Ativado").FirstOrDefault().Id ? "t" : "f");
            }
            else
            {
                return Json("n");
            }
        }

        public ActionResult NovoUser()
        {
            ViewBag.PerfilId = new SelectList(db.Perfis, "Id", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Nome");
            
            return View("NovoUser");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult NovoUser(Usuario usu, HttpPostedFileBase FotoPerfil)
        {
            string valor = "";
            var novasenha = Funcoes.SenhaAleatoria();
            usu.Senha = Funcoes.HashTexto(novasenha, "SHA512");
            usu.StatusId = db.Status.Where(s => s.Nome == "Desativado").FirstOrDefault().Id;
            if (ModelState.IsValid)
            {
                Usuario user = new Usuario
                {
                    Nome = usu.Nome,
                    Email = usu.Email,
                    Senha = usu.Senha,
                    FotoPerfil = usu.FotoPerfil,
                    PerfilId = usu.PerfilId,
                    StatusId = usu.StatusId
                };

                db.Usuarios.Add(user);
                db.SaveChanges();

                if (FotoPerfil != null)
                {
                    Funcoes.CriarDiretorio(user.Id);
                    string nomearq = "FotoPerfil" + user.Id + ".png";
                    valor = Funcoes.UploadArquivo(FotoPerfil, nomearq, user.Id);
                    if (valor == "sucesso")
                    {
                        user.FotoPerfil = "\\Areas\\Admin\\Content\\Images\\" + user.Id + "\\" + nomearq;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Usuário cadastrado com sucesso";
                        return RedirectToAction("Listar", "Usuario"); 
                    }
                    else
                    {
                        user.FotoPerfil = "\\Areas\\Admin\\Content\\Images\\anonimo.jpg";
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "warning|Problema no Upload da foto: " + valor;
                        return RedirectToAction("Listar", "Usuario");
                    }
                }
                else
                {
                    user.FotoPerfil = "\\Areas\\Admin\\Content\\Images\\anonimo.jpg";
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Usuário cadastrado com sucesso";
                    return Listar();
                }
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return Listar();
        }


        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public JsonResult RemoveUser(string id)
        {
            try
            {
                Usuario u = db.Usuarios.Find(Convert.ToInt32(id));

                if (u != null)
                {

                    if (u.Id == Convert.ToInt32(User.Identity.Name.Split('|')[0]))
                    {
                        @TempData["MSG"] = "error|Não é possível excluir seu próprio usuário!|x";
                        return Json("f");
                    } else if (u.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                    {
                        @TempData["MSG"] = "error|Não é possível excluir um usuário com status ATIVADO!|x";
                        return Json("f");
                    }

                    db.Usuarios.Remove(u);
                    db.SaveChanges();
                    @TempData["MSG"] = "success|Usuário removido com sucesso!|x";
                    return Json(u != null ? "t" : "f");

                }
                else
                {
                    @TempData["MSG"] = "error|Erro ao excluir o usuário!|x";
                    return Json("f");
                }
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Erro ao excluir o usuário!|x";
                return Json("f");
            }
        }


        public ActionResult DetalhesUser(int id)
        {
            Usuario u = db.Usuarios.Find(Convert.ToInt32(id));
            if (u != null)
            {
                ViewBag.usuarios = u;
            }
            return View();
        }

    }
}