using NGT.Data;
using NGT.Models;
using NGT.Models.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NGT.Controllers
{
    public class HomeController : Controller
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Admin.Dashboard.Index");
            }
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Acesso(Logon log)
        {
            if (ModelState.IsValid)
            {
                bool persist = log.Perm;
                string senhacrip = Funcoes.HashTexto(log.Senha, "SHA512");
                var usu = db.Usuarios.Include(u => u.Perfil).Include(u=>u.Status).Where(u => u.Email == log.Email && u.Senha == senhacrip).ToList().FirstOrDefault();
                if (usu == null)
                {
                    ModelState.AddModelError("", "Usuário/senha inválidos!");
                    return View("Index");
                }
                else
                {
                    if (usu.Status.Nome == "Desativado")
                    {
                        ModelState.AddModelError("", "Usuário ainda não foi ativado!");
                        return View("Index");
                    }
                    string permissoes = usu.Perfil.Nome;
                    FormsAuthentication.SetAuthCookie(usu.Id + "|" + usu.Nome.Split(' ')[0] + "|" + usu.Nome.Split(' ')[1] + "|" + permissoes + "|" + usu.FotoPerfil, persist);
                    //3|Rudson|Nunes|Admin|Foto
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usu.Id + "|" + usu.Nome.Split(' ')[0] + "|" + usu.Nome.Split(' ')[1] + "|" + permissoes + "|" + usu.FotoPerfil, DateTime.Now, DateTime.Now.AddMinutes(30), persist, permissoes);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    Response.Cookies.Add(cookie);

                    TempData["MSG"] = "success|Login realizado com sucesso!!!|x";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}