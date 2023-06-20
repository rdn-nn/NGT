using NGT.Application;
using NGT.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGT.Models.Entities;
using System.Drawing.Drawing2D;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Cryptography;
using System.Net.Sockets;

namespace NGT.Areas.Admin.Controllers
{
    public class OSController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Index()
        {

            ViewBag.Ordens = db.OrdServicos.Include(x => x.Fornecedor).Include(x => x.ManutencaoTipo).Include(x => x.Usuario).Include(x => x.StatusTicket).ToList();
            ViewBag.Itens = db.OSItem.Include(x => x.OrdServico).Include(x => x.ItemDesc).ToList();

            ViewBag.Pendente = db.OrdServicos.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Pendente").Count();
            ViewBag.Andamento = db.OrdServicos.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Em Andamento").Count();
            ViewBag.Concluido = db.OrdServicos.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Concluído").Count();
            ViewBag.Cancelado = db.OrdServicos.Include(x => x.StatusTicket).Where(x => x.StatusTicket.Nome == "Cancelado").Count();

            return View("OS_Lista");
        }
        public ActionResult Criar()
        {
            ViewBag.ItemDescId = new SelectList(db.ItemDescs.Include(x => x.Item).Include(x => x.Status).Where(x => x.Status.Nome == "Ativado").OrderBy(x => x.Item.Nome
           ), "Id", "ItemDescInfo");
            ViewBag.FornecedorId = new SelectList(db.Fornecedores.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "NomeFantasia");
            ViewBag.ManutencaoTipoId = new SelectList(db.ManutencaoTipo.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome");
            return View("NovaOS");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Criar(OrdServico ors, FormCollection itemCollection)
        {

            if (ModelState.IsValid)
            {
                var selectedValues = itemCollection["ItemDescId"];
                List<int> listaItemDescIds = selectedValues.Split(',').Select(int.Parse).ToList();

                var idors = 0;
                if (db.OrdServicos.FirstOrDefault() != null)
                {
                    idors = db.OrdServicos.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    idors++;
                }
                else
                {
                    idors++;
                }

                var ticket = "OS" + idors + DateTime.Now.Day + DateTime.Now.Second;

                if (ors.Obs != null)
                {
                    ors.Obs = ors.Obs + " - Data: " + DateTime.Now;
                }

                OrdServico ordem = new OrdServico
                {
                    DescProblema = ors.DescProblema,
                    NumTicketOS = ticket,
                    Obs = ors.Obs,
                    FornecedorId = ors.FornecedorId,
                    Valor = ors.Valor,
                    ManutencaoTipoId = ors.ManutencaoTipoId,
                    NotaF = ors.NotaF,
                    CentroCusto = ors.CentroCusto,
                    DataEntregaPrevis = ors.DataEntregaPrevis,
                    UsuarioId = Convert.ToInt32(User.Identity.Name.Split('|')[0]),
                    StatusTicketId = db.StatusTickets.Where(x => x.Nome == "Pendente").FirstOrDefault().Id
                };

                db.OrdServicos.Add(ordem);
                db.SaveChanges();

                idors = db.OrdServicos.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                listaItemDescIds.Add(idors);

                TempData["MSG"] = "success|Sua ordem de serviço foi registrado com sucesso!|" + ticket;
                TempData["listaIds"] = listaItemDescIds;
                return RedirectToAction("Index", "OSItem");
            }
            TempData["MSG"] = "warning|Preencha todos os campos|x";
            return View("OS_Lista");
        }

        public ActionResult EditaOS(string id)
        {

            OrdServico ors = db.OrdServicos.Find(Convert.ToInt32(id));
            if (ors != null)
            {
                ViewBag.ordens = ors;
                ViewBag.Itens = db.OSItem.Include(x => x.OrdServico).Include(x => x.ItemDesc).ToList();
                List<int> selectedItems = new List<int>();
                foreach (var item in ViewBag.Itens)
                {
                    if (item.OrdServicoId == ors.Id)
                    {
                        selectedItems.Add(item.ItemDescId);
                    }
                }

                ViewBag.SelectedItems = new SelectList(selectedItems);
                ViewBag.ItemDescId = new SelectList(db.ItemDescs.Include(x => x.Item).Include(x => x.Status).Where(x => x.Status.Nome == "Ativado").OrderBy(x => x.Item.Nome
           ), "Id", "ItemDescInfo");

                ViewBag.FornecedorId = new SelectList(db.Fornecedores.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "NomeFantasia", ors.FornecedorId);
                ViewBag.ManutencaoTipoId = new SelectList(db.ManutencaoTipo.Include(x => x.Status).Where(x => x.Status.Nome == "Ativado"), "Id", "Nome", ors.ManutencaoTipoId);

                ViewBag.StatusTicketId = new SelectList(db.StatusTickets.OrderByDescending(s => s.Id), "Id", "Nome", ors.StatusTicketId);

            }
            return View("EditaOS");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditaOS(OrdServico ors, FormCollection itemCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OrdServico ordem = db.OrdServicos.Find(ors.Id);

                    ordem.DescProblema = ors.DescProblema;
                    ordem.FornecedorId = ors.FornecedorId;
                    ordem.Valor = ors.Valor;
                    ordem.ManutencaoTipoId = ors.ManutencaoTipoId;
                    ordem.NotaF = ors.NotaF;
                    ordem.CentroCusto = ors.CentroCusto;
                    ordem.DataEntregaPrevis = ors.DataEntregaPrevis;
                    ordem.DataEntregaReal = ors.DataEntregaReal;
                    ordem.DataAtualizacao = DateTime.Now;

                    if (ors.Obs != null)
                    {
                        if (ordem.Obs != null)
                        {
                            ordem.Obs = "● " + ors.Obs + " - Data: " + ordem.DataAtualizacao + " | " + ordem.Obs;
                        }
                        else
                        {
                            ordem.Obs = "● " + ors.Obs + " - Data: " + ordem.DataAtualizacao;
                        }
                    }

                    ordem.StatusTicketId = ors.StatusTicketId;

                    db.Entry(ordem).State = EntityState.Modified;
                    db.SaveChanges();

                    var idors = ors.Id;
                    var selectedValues = itemCollection["ItemDescId"];
                    List<int> listaItemDescIds = selectedValues.Split(',').Select(int.Parse).ToList();
                    listaItemDescIds.Add(idors);

                    TempData["MSG"] = "success|Sua ordem de serviço foi atualizada com sucesso!";
                    TempData["listaIds"] = listaItemDescIds;
                    return RedirectToAction("Index", "OSItem");
                }
                TempData["MSG"] = "warning|Preencha todos os campos|x";
                return RedirectToAction("Index", "OS");

            }
            catch (Exception)
            {
                @TempData["MSG"] = "error|Não foi possível realizar as alterações. Tente novammente!|x";
                return View("OS_Lista");
            }
        }

    }
}