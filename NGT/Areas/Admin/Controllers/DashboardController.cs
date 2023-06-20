using NGT.Application;
using NGT.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NGT.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var pizza2Result = db.OrdServicos.Include(x => x.StatusTicket).GroupBy(x => x.StatusTicket.Nome).Select(g => new { g.Key, TotalOrdens = g.Sum(x => 1) });
            string pizza2Dados = "";
            foreach (var item in pizza2Result)
            {
                pizza2Dados +=
                "{name: '" + item.Key + "', y: " + item.TotalOrdens.ToString().Replace(",", ".") + "},";
            }
            if (pizza2Dados != "")
            {
                pizza2Dados = pizza2Dados.Substring(0, pizza2Dados.Length - 1);
            }

            ViewBag.GraficoPizza2 = Funcoes.GerarGraficoPizza2("Total OS por Status", pizza2Dados);

            //agrupa ano
            var colResult = db.OrdServicos.Include(x => x.Fornecedor).Where(x => x.DataEntregaReal != null && x.StatusTicket.Nome != "Cancelado").GroupBy(x => new { Nome = x.Fornecedor.NomeFantasia, Ano = x.DataEntregaReal.Value.Year }).Select(g => new { g.Key.Nome, g.Key.Ano, total = g.Sum(x => x.Valor) }).OrderByDescending(x => x.Ano);

            var anoAtual = 0;
            string colDados = "";
            foreach (var item in colResult)
            {
                //if (item.Ano != 0)
                //{
                anoAtual = item.Ano;
                colDados += "{name: '" + item.Nome + "', y: " + item.total.ToString().Replace(",", ".") + ", drilldown: '" + item.Nome + "'},";
                //}
            }
            if (colDados != "")
            {
                colDados = colDados.Substring(0, colDados.Length - 1);
            }

            //agrupa meses do ano
            var colResult2 = db.OrdServicos.Include(x => x.Fornecedor).Where(x => x.DataEntregaReal != null && x.DataEntregaReal.Value.Year == anoAtual && x.StatusTicket.Nome != "Cancelado").GroupBy(x => new { Nome = x.Fornecedor.NomeFantasia, Mes = x.DataEntregaReal.Value.Month }).Select(g => new { g.Key.Nome, g.Key.Mes, total = g.Sum(x => x.Valor) }).OrderBy(x => x.Mes);
            var name = "";
            string colDados2 = "";
            foreach (var item in colResult2)
            {
                

                if (name != item.Nome)
                {
                    name = item.Nome;
                    if (colDados2 != "")
                    {
                        colDados2 = colDados2.Substring(0, colDados2.Length - 1);
                        colDados2 += "]},";
                    }
                    
                    colDados2 += "{name: '" + item.Nome + "', id: '" + item.Nome + "', data: [";
                }
                var mes = "";
                switch (item.Mes){
                        case 01:
                        mes = "Jan";
                    break;
                        case 02:
                        mes = "Fev";
                    break;
                        case 03:
                        mes = "Mar";
                        break;
                    case 04:
                        mes = "Abr";
                        break;
                    case 05:
                        mes = "Mai";
                        break;
                    case 06:
                        mes = "Jun";
                        break;
                    case 07:
                        mes = "Jul";
                        break;
                    case 08:
                        mes = "Ago";
                        break;
                    case 09:
                        mes = "Set";
                        break;
                    case 10:
                        mes = "Out";
                        break;
                    case 11:
                        mes = "Nov";
                        break;
                    case 12:
                        mes = "Dez";
                        break;
                }

                colDados2 += "['" + mes + "', " + item.total.ToString().Replace(",", ".") + "],";

            }
            if (colDados2 != "") {
                colDados2 = colDados2.Substring(0, colDados2.Length - 1);
                colDados2 += "]}";
            }
            
            ViewBag.GraficoColunas = Funcoes.GerarGraficoColunas("Total OS por Status", colDados, colDados2, anoAtual);


            //agrupa tipo
            var barResult = db.OrdServicos.Include(x => x.ManutencaoTipo).Where(x => x.CentroCusto != null && x.StatusTicket.Nome != "Cancelado").GroupBy(x => x.ManutencaoTipo.Nome).Select(g => new { g.Key }).OrderBy(x => x.Key).ToList();

            //agrupa tipo e centro de custo
            var barResult2 = db.OrdServicos.Include(x => x.ManutencaoTipo).Where(x => x.CentroCusto != null && x.StatusTicket.Nome != "Cancelado").GroupBy(x => new { CC = x.CentroCusto, Tipo = x.ManutencaoTipo.Nome }).Select(g => new { g.Key.CC, g.Key.Tipo, Total = g.Sum(x => x.Valor) }).OrderBy(x => x.CC);

            var cc = "";
            string barDados2 = "";
            string barDados = "";
            var barResult3 = barResult;
            var cont = 0;
            foreach (var item1 in barResult)
            {
                barDados += "'" + item1.Key + "',";
            }

            foreach (var item2 in barResult2)
            {

                if (cc != item2.CC)
                {
                    cc = item2.CC;
                    if (barDados2 != "")
                    {
                        barDados2 = barDados2.Substring(0, barDados2.Length - 1);
                        barDados2 += "]},";
                    }

                    barDados2 += "{name: '" + item2.CC + "', data: [";
                    cont = 0;
                }
                for (var item3 = cont; item3 < barResult3.Count(); item3++)
                {
                    cont++;
                    if (barResult3[item3].Key == item2.Tipo)
                    {
                        break;
                    }
                    else
                    {
                        barDados2 +=  0 + ",";
                    }
                }
                barDados2 += item2.Total.ToString().Replace(",", ".") + ",";
            }

            if (barDados != "")
            {
                barDados = barDados.Substring(0, barDados.Length - 1);
            }
            if (barDados2 != "")
            {
                barDados2 = barDados2.Substring(0, barDados2.Length - 1);
                barDados2 += "]}";
            }
            ViewBag.GraficoBarras = Funcoes.GerarGraficoBarras("Total OS por Status", barDados, barDados2);

            return View("Dashboard");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home.Index");
        }
    }
}