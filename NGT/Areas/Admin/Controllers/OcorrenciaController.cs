using NGT.Application;
using NGT.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;

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

            ViewBag.Pendente = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Pendente").Count();
            ViewBag.Andamento = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Andamento").Count();
            ViewBag.Concluido = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Concluído").Count();
            ViewBag.Cancelado = db.Ocorrencias.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Cancelado").Count();
            return View("Ocorrencia");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Criar(Ocorrencia Oco)
        {
            if (ModelState.IsValid)
            {
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
                    BlocoId = Oco.BlocoId,
                    LocalId = Oco.LocalId,
                    CategoriaId = Oco.CategoriaId,
                    ItemId = Oco.ItemId,
                    MotivoId = Oco.MotivoId,
                    StatusTicketId = db.StatusTickets.Where(x => x.Nome == "Pendente").FirstOrDefault().Id
                };

                db.Ocorrencias.Add(ocorrencia);
                db.SaveChanges();
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
    }
}