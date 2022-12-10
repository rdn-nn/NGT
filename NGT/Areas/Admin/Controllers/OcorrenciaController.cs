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

namespace NGT.Areas.Admin.Controllers
{
    public class OcorrenciaController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        // GET: Admin/Ocorrencia
        public ActionResult Index()
        {
            ViewBag.Ocorrencias = db.Ocorrencias.Include(x => x.Bloco).Include(x => x.Categoria).Include(x => x.Item).Include(x => x.Local).Include(x => x.Motivo).Include(x => x.StatusTicket).ToList();

            ViewBag.BlocoId = new SelectList(db.Blocos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.MotivoId = new SelectList(db.Motivos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");

            ViewBag.StatusTicketsId = db.StatusTickets.ToList();

            ViewBag.Pendente = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Pendente").Count();
            ViewBag.Andamento = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Em Andamento").Count();
            ViewBag.Concluido = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Concluído").Count();
            ViewBag.Cancelado = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Cancelado").Count();

            return View("Ocorrencia");
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
            // Filter the states by country. For example:

            var itens = (from i in db.Itens.Include(x => x.Local).Include(x => x.Categoria).Include(x => x.Status)
                         where i.Local.Id == localId && i.Categoria.Id == categId && i.Status.Nome == "Ativado"
                         select new
                         {
                             id = i.Id,
                             nome = i.Nome
                         }).ToArray();
            return Json(itens);
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
        public ActionResult TrocarStatus(int Id, int StatusTicketId)
        {
            try
            {
                Ocorrencia oco = db.Ocorrencias.Find(Convert.ToInt32(Id));
                if(oco.StatusTicketId != StatusTicketId)
                {
                    if (oco != null)
                    {
                        oco.StatusTicketId = Convert.ToInt32(StatusTicketId);
                        oco.DataAtualizacao = DateTime.Now;

                        db.Entry(oco).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["MSG"] = "success|Status alterado com sucesso!|x";
                        if (oco.Email != null)
                        {
                            string msg = "<h3>Sistema NewGen Tech</h3>";
                            msg += "<p>Seu chamado " + oco.NumTicket + " teve uma atualização. </p><p>Para consultar o seu chamado, utilize o código abaixo no campo de consulta do site:</p><p> Chamado registrado - " + oco.NumTicket + " </p>";
                            Funcoes.EnviarEmail(oco.Email, "Alteração no seu chamado!", msg);
                        }

                        return Index();
                    }
                    TempData["MSG"] = "warning|Preencha todos os campos!|x";
                    return Index();
                } else
                {
                    TempData["MSG"] = "info|Nenhuma alteração de status foi realizada!|x";
                    return Index();
                }
            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Não foi possível alterar o status. Tente novammente!|x";
                return Index();
            }
        }

    }
}