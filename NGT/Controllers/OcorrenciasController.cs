using NGT.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;

namespace NGT.Controllers
{
    
    public class OcorrenciasController : Controller
    {
        private NgtContexto db = new NgtContexto();

        public ActionResult Criar()
        {
            ViewBag.BlocoId = new SelectList(db.Blocos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.MotivoId = new SelectList(db.Motivos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            return View("NovaOcorrencia");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Criar(Ocorrencia Oco, HttpPostedFileBase FotoOcorrencia)
        {
            if (ModelState.IsValid)
            {
                string valor = "";
                var nomeBloco = db.Blocos.Where(x => x.Id == Oco.BlocoId).FirstOrDefault().Nome;
                var letraBloco = nomeBloco.Substring(nomeBloco.Length - 1, 1);
                var idoco = 0;
                if (db.Ocorrencias.FirstOrDefault() != null)
                {
                    idoco = db.Ocorrencias.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    idoco++;
                }

                var ticket = letraBloco + idoco + DateTime.Now.Day + DateTime.Now.Second;

                Ocorrencia ocorrencia = new Ocorrencia();
                ocorrencia.Obs = Oco.Obs;
                ocorrencia.NumTicket = ticket;
                ocorrencia.FotoOcorrencia = "\\Areas\\Admin\\Content\\Images\\imgNoFound.png";
                ocorrencia.Email = Oco.Email;
                ocorrencia.BlocoId = Oco.BlocoId;
                ocorrencia.LocalId = Oco.LocalId;
                ocorrencia.CategoriaId = Oco.CategoriaId;
                ocorrencia.ItemId = Oco.ItemId;
                ocorrencia.MotivoId = Oco.MotivoId;
                ocorrencia.StatusTicketId = db.StatusTickets.Where(x => x.Nome == "Pendente").FirstOrDefault().Id;

                db.Ocorrencias.Add(ocorrencia);
                db.SaveChanges();

                if (FotoOcorrencia != null)
                {
                    //Funcoes.CriarDiretorio(ocorrencia.NumTicket);
                    string nomearq = "FotoOcorrencia" + ocorrencia.NumTicket + ".png";
                    valor = Funcoes.UploadArquivo(FotoOcorrencia, nomearq, ocorrencia.NumTicket);
                    if (valor == "sucesso")
                    {
                        ocorrencia.FotoOcorrencia = "\\Areas\\Admin\\Content\\Images\\" + ocorrencia.NumTicket + "\\" + nomearq;
                        db.Entry(ocorrencia).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Seu chamado foi registrado com sucesso!|" + ticket;
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["MSG"] = "success|Seu chamado foi registrado com sucesso!|" + ticket;
                if (Oco.Email != null)
                {
                    string msg = "<h3>Sistema NewGen Tech</h3>";
                    msg += "<p>Seu chamado foi registrado com sucesso! Para consultar o seu chamado, utilize o código abaixo no campo de consulta do site:</p><p> Chamado registrado - " + ticket + " </p>";
                    Funcoes.EnviarEmail(Oco.Email, "Registro de Chamado no sistema da Fatec", msg);
                }

                return RedirectToAction("Index", "Home");
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult CarregaLocal(int blocoId)
        {
            var locais = (from l in db.Locais.Include(x => x.Bloco).Include(x => x.Status)
                          where l.Bloco.Id == blocoId && l.Status.Nome == "Ativado"
                          select new
                          {
                              id = l.Id,
                              nome = l.Nome
                          }).ToArray();
            return Json(locais);
        }

        [HttpPost]
        public ActionResult CarregaItem(int localId, int categId)
        {

            var itens = (from i in db.ItemDescs.Include(x => x.Item).Include(x => x.Local).Include(x => x.Categoria).Include(x => x.Status)
                         where i.Local.Id == localId && i.CategoriaId == categId && i.Status.Nome == "Ativado"
                         select new
                         {
                             id = i.Item.Id,
                             nome = i.Item.Nome,
                         }).Distinct().ToArray();
            return Json(itens);
        }

        [HttpGet]
        public ActionResult ConsultaChamado(string termo)
        {
            if (termo == null)
            {
                return View("ConsultaChamado");
            } 
            else
            {
                var encontrado = db.Ocorrencias.Where(x => x.NumTicket == termo).ToList().FirstOrDefault();
                //TempData["LP"] = "x";
                return PartialView("_CarregaChamado", encontrado);
            }

        }


        //Rotas de Carregamento Mobile
        
        [HttpPost]
        public ActionResult BlocoMob()
        {
            //ViewBag.Ocorrencias = db.Ocorrencias.Include(x => x.Bloco).Include(x => x.Categoria).Include(x => x.Item).Include(x => x.Local).Include(x => x.Motivo).Include(x => x.StatusTicket).ToList();
            //var bloco = new SelectList(db.Blocos.Include(x => x.Status)).ToList(); 
            var blocos = (from b in db.Blocos.Include(x => x.Status)
                          where b.Status.Nome == "Ativado"
                          select new
                          {
                              id = b.Id,
                              nome = b.Nome
                          }).ToList();
            return Json(blocos, JsonRequestBehavior.AllowGet);
        }

    }
}