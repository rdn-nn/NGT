using NGT.Application;
using NGT.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;
using System.Net.Sockets;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace NGT.Areas.Admin.Controllers
{
    public class OcorrenciaController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        // GET: Admin/Ocorrencia
        public ActionResult Index()
        {
            ViewBag.Ocorrencias = db.Ocorrencias.Include(x => x.Bloco).Include(x => x.Categoria).Include(x => x.Item).Include(x => x.Local).Include(x => x.Motivo).Include(x => x.StatusTicket).ToList();

            //ViewBag.StatusTicketsId = db.StatusTickets.ToList();

            ViewBag.Pendente = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Pendente").Count();
            ViewBag.Andamento = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Em Andamento").Count();
            ViewBag.Concluido = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Concluído").Count();
            ViewBag.Cancelado = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Cancelado").Count();

            return View("Ocorrencia");
        }

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
                var email = db.Usuarios.Find(Convert.ToInt32(User.Identity.Name.Split('|')[0]));
                var nomeBloco = db.Blocos.Where(x => x.Id == Oco.BlocoId).FirstOrDefault().Nome;
                var letraBloco = nomeBloco.Substring(nomeBloco.Length - 1, 1);
                var idoco = 0;
                if (db.Ocorrencias.FirstOrDefault() != null)
                {
                    idoco = db.Ocorrencias.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    idoco++;
                }

                var ticket = letraBloco + idoco + DateTime.Now.Day + DateTime.Now.Second;

                Ocorrencia ocorrencia = new Ocorrencia
                {
                    Obs = Oco.Obs,
                    NumTicket = ticket,
                    FotoOcorrencia = Oco.FotoOcorrencia,
                    BlocoId = Oco.BlocoId,
                    LocalId = Oco.LocalId,
                    CategoriaId = Oco.CategoriaId,
                    ItemId = Oco.ItemId,
                    MotivoId = Oco.MotivoId,
                    Email = email.Email,
                    StatusTicketId = db.StatusTickets.Where(x => x.Nome == "Pendente").FirstOrDefault().Id
                };

                db.Ocorrencias.Add(ocorrencia);
                db.SaveChanges();

                if (FotoOcorrencia != null)
                {
                    Funcoes.CriarDiretorio(ocorrencia.NumTicket);
                    string nomearq = "FotoOcorrencia" + ocorrencia.NumTicket + ".png";
                    valor = Funcoes.UploadArquivo(FotoOcorrencia, nomearq, ocorrencia.NumTicket);
                    if (valor == "sucesso")
                    {
                        ocorrencia.FotoOcorrencia = "\\Areas\\Admin\\Content\\Images\\" + ocorrencia.NumTicket + "\\" + nomearq;
                        db.Entry(ocorrencia).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Seu chamado foi registrado com sucesso!|" + ticket;
                        return RedirectToAction("Index", "Ocorrencia");
                    }
                }

                TempData["MSG"] = "success|Seu chamado foi registrado com sucesso!|" + ticket;
                return RedirectToAction("Index", "Ocorrencia");
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return RedirectToAction("Index", "Ocorrencia");
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
            //ViewBag.ItemId = new SelectList(db.ItemDescs.Include(x => x.Item).Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Item.Nome");
        }

        public ActionResult TrocarStatus(string id)
        {
            Ocorrencia oco = db.Ocorrencias.Find(Convert.ToInt32(id));
            if (oco != null)
            {
                ViewBag.ocorencia = oco;
                ViewBag.StatusTicketId = new SelectList(db.StatusTickets.OrderByDescending(s => s.Id), "Id", "Nome", oco.StatusTicketId);

            }
            return View("EditaOcorrencia");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult TrocarStatus(int Id, int StatusTicketId, string InfoAtualizacao)
        {
            try
            {
                Ocorrencia oco = db.Ocorrencias.Find(Convert.ToInt32(Id));
                if (oco != null)
                {
                    if (oco.StatusTicketId != StatusTicketId || InfoAtualizacao != "")
                    {
                        oco.StatusTicketId = StatusTicketId;
                        oco.DataAtualizacao = DateTime.Now;
                        if (InfoAtualizacao != "")
                        {
                            if (oco.InfoAtualizacao != null)
                            {
oco.InfoAtualizacao = InfoAtualizacao + " Data: " + oco.DataAtualizacao + "   |   " + oco.InfoAtualizacao;
                            } else
                            {
                                oco.InfoAtualizacao = InfoAtualizacao + " Data: " + oco.DataAtualizacao;
                            }
                            
                        }
                        db.Entry(oco).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["MSG"] = "success|Status alterado com sucesso!|x";
                        if (oco.Email != null)
                        {
                            string msg = "<h3>Sistema NewGen Tech</h3>";
                            msg += "<p>Seu chamado " + oco.NumTicket + " teve uma atualização. </p>";
                            if (InfoAtualizacao != "")
                            {
                                msg += "<p>Comentário adicionado: " + oco.InfoAtualizacao + "</p>";
                            }
                                msg += "<p>Status Atualizado: " + oco.StatusTicket.Nome + "</p>";
                            msg +="<p>Para consultar o seu chamado, utilize o código abaixo no campo de consulta do site:</p><p> Chamado registrado - " + oco.NumTicket + " </p>";
                            Funcoes.EnviarEmail(oco.Email, "Alteração no seu chamado!", msg);
                        }
                        return Index();
                    }
                    else
                    {
                        TempData["MSG"] = "info|Nenhuma alteração de status foi realizada!|x";
                        return Index();
                    }

                }
                TempData["MSG"] = "warning|Preencha todos os campos!|x";
                return Index();
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Não foi possível alterar o status. Tente novammente!|x";
                return Index();
            }
        }

    }
}