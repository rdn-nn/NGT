using ImageResizer.Configuration.Xml;
using NGT.Application;
using NGT.Data;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;


namespace NGT.Areas.Admin.Controllers
{
    public class UsuarioController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Listar()
        {

            ViewBag.usuarios = db.Usuarios.Include(x => x.Perfil).ToList().OrderBy(u => u.Nome);
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
            @TempData["LP"] = "x";
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

                user.Hash = Funcoes.Codifica(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                string msg = "<h3>Sistema NewGen Tech</h3>";
                msg += "<p>Você está recebendo sua senha de acesso para entrar no sistema de gestão da Fatec. Acesse o site <a href='https://localhost:44367/ativarusuario/" + user.Hash + "' target='_blank'>clicando aqui</a> para ativar seu usuário e entrar com sua senha e usuário cadastrados, indicados abaixo:</p><p>Usuario: " + user.Email + "</p><p> Senha: " + novasenha + " </p>";
                Funcoes.EnviarEmail(user.Email, "Senha de Acesso", msg);


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
                string newId = "";
                if (id.Contains("_"))
                {
                    newId = id.Substring(7);
                }
                else
                {
                    newId = id;
                }


                Usuario u = db.Usuarios.Find(Convert.ToInt32(newId));

                if (u != null)
                {

                    if (u.Id == Convert.ToInt32(User.Identity.Name.Split('|')[0]))
                    {
                        @TempData["MSG"] = "error|Não é possível excluir seu próprio usuário!|x";
                        return Json("f");
                    }

                    if (id != "remove_" + u.Id)
                    {
                        if (u.StatusId == db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id)
                        {
                            @TempData["MSG"] = "error|Não é possível excluir um usuário com status ATIVADO!|x";
                            return Json("f");
                        }
                    }


                    int tamPath = u.FotoPerfil.Length;
                    int tamImg = u.FotoPerfil.Split('\\').Last().Length;
                    string path = u.FotoPerfil.Substring(0, tamPath - tamImg - 1);
                    if (u.FotoPerfil.Split('\\').Last() != "anonimo.jpg")
                    {
                        path = HttpRuntime.AppDomainAppPath + path;
                        if (Directory.Exists(path))
                        {
                            Directory.Delete(path, true);
                        }

                    }
                    //if you want to get virtual application path, you could try the code below.
                    //HttpContext.Current.Request.ApplicationPath;
                    //Or the code
                    //HttpRuntime.AppDomainAppVirtualPath;
                    //        But if you want to get physical application path, the following code is fit for you.
                    //        HttpRuntime.AppDomainAppPath
                    //        Or convert the virtual path to physical path
                    //HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath)
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

        public ActionResult EditaUser(string id)
        {
            Usuario u = db.Usuarios.Find(Convert.ToInt32(id));
            if (u != null)
            {
                ViewBag.Usuario = u;

                ViewBag.StatusId = new SelectList(db.Status.OrderByDescending(s => s.Id), "Id", "Nome", u.Status.Id);

                ViewBag.PerfilId = new SelectList(db.Perfis.OrderBy(s => s.Id), "Id", "Nome", u.Perfil.Id);

            }
            return View("EditUser");
        }

        [HttpPost, ValidateAntiForgeryToken, Obsolete]
        public ActionResult EditaUser(Usuario usu, HttpPostedFileBase FotoPerfil)
        {
            string valor = "";
            if (ModelState.IsValid)
            {
                Usuario u = db.Usuarios.Find(usu.Id);

                u.Nome = usu.Nome;
                u.Email = usu.Email;
                //u.Senha = usu.Senha,
                //u.FotoPerfil = usu.FotoPerfil;
                u.PerfilId = usu.PerfilId;
                u.StatusId = usu.StatusId;

                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();

                if (FotoPerfil != null)
                {
                    Funcoes.CriarDiretorio(u.Id);
                    string nomearq = "FotoPerfil" + u.Id + ".png";
                    valor = Funcoes.UploadArquivo(FotoPerfil, nomearq, u.Id);
                    if (valor == "sucesso")
                    {
                        u.FotoPerfil = "\\Areas\\Admin\\Content\\Images\\" + u.Id + "\\" + nomearq;
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Usuário atualizado com sucesso";
                        return RedirectToAction("Listar", "Usuario");
                    }
                }
                TempData["MSG"] = "success|Usuário atualizado com sucesso";
                return RedirectToAction("Listar", "Usuario");
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return Listar();
        }

        public ActionResult AlteraSenha(string id)
        {
            Usuario u = db.Usuarios.Find(Convert.ToInt32(id));
            if (u != null)
            {
                ViewBag.Usuario = u;
            }
            return View("AlteraSenhaUser");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AlteraSenha(string id, string passwordAtual, string passwordNovo, string novoRepete)
        {
            if (ModelState.IsValid)
            {
                Usuario u = db.Usuarios.Find(Convert.ToInt32(id));
                if (u != null)
                {
                    var senhaAtual = Funcoes.HashTexto(passwordAtual, "SHA512");
                    if (senhaAtual == u.Senha)
                    {
                        var novaSenha = Funcoes.HashTexto(passwordNovo, "SHA512");

                        u.Senha = novaSenha;

                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Senha de usuário atualizada com sucesso";
                        return RedirectToAction("Index", "Dashboard");
                    }
                    TempData["MSG"] = "error|Senha atual incorreta. Tente novamente.";
                    return RedirectToAction("Index", "Dashboard");
                }
                TempData["MSG"] = "error|Usuário não encontrado no cadastro";
                return RedirectToAction("Index", "Dashboard");
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return RedirectToAction("Index", "Dashboard");
        }
    }

}
