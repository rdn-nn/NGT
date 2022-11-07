using NGT.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace NGT.Controllers
{
    public class OcorrenciaController : Controller
    {
        private NgtContexto db = new NgtContexto();
        // GET: Ocorrencia
        public ActionResult Criar()
        {
            ViewBag.BlocoId = new SelectList(db.Blocos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categorias.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            ViewBag.MotivoId = new SelectList(db.Motivos.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            return View("NovaOcorrencia");
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

                Ocorrencia ocorrencia = new Ocorrencia();
                ocorrencia.Obs = Oco.Obs;
                ocorrencia.NumTicket = ticket;
                ocorrencia.BlocoId = Oco.BlocoId;
                ocorrencia.LocalId = Oco.LocalId;
                ocorrencia.CategoriaId = Oco.CategoriaId;
                ocorrencia.ItemId = Oco.ItemId;
                ocorrencia.MotivoId = Oco.MotivoId;
                ocorrencia.StatusTicketId = db.StatusTickets.Where(x => x.Nome == "Pendente").FirstOrDefault().Id;
                //public string Obs { get; set; }
                //public string Imagem { get; set; }
                //public DateTime DataCriacao { get; set; }
                //public DateTime? DataAtualizacao { get; set; }
                //public string NumTicket { get; set; }
                //[Required]


                //ocorrencia.Bloco = db.Blocos.FirstOrDefault(b => b.Id == Bloco);
                //ocorrencia.Local = db.Locais.FirstOrDefault(l => l.Id == Local);
                //ocorrencia.Categoria = db.Categorias.FirstOrDefault(c => c.Id == Categoria);
                //ocorrencia.Item = db.Itens.FirstOrDefault(i => i.Id == Item);
                //ocorrencia.Motivo = motivo;
                //ocorrencia.Obs = obs;
                //ocorrencia.Status = Models.Entities.Ocorrencia.Boolean.PENDENTE;
                //ocorrencia.Recorrencia = 1;

                db.Ocorrencias.Add(ocorrencia);
                db.SaveChanges();
                TempData["MSG"] = "success|Seu chamado foi registrado com sucesso!|" + ticket ;
                return RedirectToAction("Index","Home");
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