﻿using NGT.Data;
using NGT.Models;
using NGT.Models.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

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

        public ActionResult Acesso()
        {
            return View("Login");
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
                    TempData["MSG"] = "error|Usuário/senha inválidos! Tente novamente.|x";
                    return View("Index");
                }
                else
                {
                    if (usu.Status.Nome == "Desativado")
                    {
                        TempData["MSG"] = "error|Usuário ainda não foi ativado!|x";
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
            TempData["MSG"] = "error|Usuário/senha inválidos! Tente novamente.|x";
            return RedirectToAction("Index");
        }

        public ActionResult AtivaUser(string hash)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(Funcoes.Decodifica(hash));
                if (dt > DateTime.Now)
                {
                    Usuario usu = db.Usuarios.Where(u => u.Hash == hash).ToList().FirstOrDefault();
                    usu.StatusId = db.Status.Where(s => s.Nome == "Ativado").FirstOrDefault().Id;
                    usu.Hash = "";
                    db.Entry(usu).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Usuário ativado com sucesso!!!|x";
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "warning|Esse link já expirou!|x";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MSG"] = "error|Hash inválida!|x";
                return RedirectToAction("Index");
            }
        }
    }
}