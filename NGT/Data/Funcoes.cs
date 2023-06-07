using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NGT.Data
{
    public class Funcoes
    {
        public static string HashTexto(string texto, string nomeHash)
        {
            HashAlgorithm algoritmo = HashAlgorithm.Create(nomeHash);
            if (algoritmo == null)
            {
                throw new ArgumentException("Nome de hash incorreto", "nomeHash");
            }
            byte[] hash = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return Convert.ToBase64String(hash);
        }

        public static string SenhaAleatoria()
        {

            string caracSenha = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-~!@#$%^&*()";
            char[] chars = new char[13];
            Random rd = new Random();
            for (int i = 0; i < 13; i++)
            {
                chars[i] = caracSenha[rd.Next(0, caracSenha.Length)];
            }
            string senhagerada = new string(chars);
            return senhagerada;

        }


        public static bool CriarDiretorio(int id)
        {
            string dir = HttpContext.Current.Request.PhysicalApplicationPath + "\\Areas\\Admin\\Content\\Images\\" + id;
            if (!Directory.Exists(dir))
            {
                //Caso não exista devermos criar
                Directory.CreateDirectory(dir);
                return true;
            }
            else
                return false;
        }
        public static bool CriarDiretorio(string id)
        {
            string dir = HttpContext.Current.Request.PhysicalApplicationPath + "\\Areas\\Admin\\Content\\Images\\" + id;
            if (!Directory.Exists(dir))
            {
                //Caso não exista devermos criar
                Directory.CreateDirectory(dir);
                return true;
            }
            else
                return false;
        }
        //public static bool ExcluirArquivo(string arq)
        //{
        //    if (File.Exists(arq))
        //    {
        //        File.Delete(arq);
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        [Obsolete]
        public static string UploadArquivo(HttpPostedFileBase flpUpload, string nome, int id)
        {
            try
            {
                double permitido = 5120;
                if (flpUpload != null)
                {

                    string arq = Path.GetFileName(flpUpload.FileName);
                    double tamanho = Convert.ToDouble(flpUpload.ContentLength) / 1024;
                    string extensao = Path.GetExtension(flpUpload.FileName).ToLower();
                    string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + "\\Areas\\Admin\\Content\\Images\\" + id + "\\" + nome;
                    if (tamanho > permitido)
                        return "Tamanho Máximo permitido é de " + permitido + " kb!";
                    else if ((extensao != ".png" && extensao != ".jpg" && extensao != ".jpeg"))
                        return "Extensão inválida, só são permitidas .png e .jpg!";
                    else
                    {
                        CriarDiretorio(id);
                        ImageResizer.ImageJob img = new ImageResizer.ImageJob(flpUpload, diretorio, new ImageResizer.ResizeSettings("width=500;height=500;format=jpg;mode=pad"));
                        img.Build();
                        //diretorio = img.FinalPath;
                        //flpUpload.SaveAs(diretorio);

                        //flpUpload.SaveAs(diretorio);
                        return "sucesso";
                    }
                }
                else
                    return "Erro no Upload!";
            }
            catch { return "Erro no Upload"; }
        }
        [Obsolete]
        public static string UploadArquivo(HttpPostedFileBase flpUpload, string nome, string id)
        {
            try
            {
                double permitido = 5120;
                if (flpUpload != null)
                {

                    string arq = Path.GetFileName(flpUpload.FileName);
                    double tamanho = Convert.ToDouble(flpUpload.ContentLength) / 1024;
                    string extensao = Path.GetExtension(flpUpload.FileName).ToLower();
                    string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + "\\Areas\\Admin\\Content\\Images\\" + id + "\\" + nome;
                    if (tamanho > permitido)
                        return "Tamanho Máximo permitido é de " + permitido + " kb!";
                    else if ((extensao != ".png" && extensao != ".jpg" && extensao != ".jpeg"))
                        return "Extensão inválida, só são permitidas .png e .jpg!";
                    else
                    {
                        CriarDiretorio(id);
                        //ImageResizer.ImageJob img = new ImageResizer.ImageJob(flpUpload, diretorio, new ImageResizer.ResizeSettings("width=500;height=500;format=jpg;mode=pad"));
                        //img.Build();
                        //diretorio = img.FinalPath;
                        flpUpload.SaveAs(diretorio);

                        //flpUpload.SaveAs(diretorio);
                        return "sucesso";
                    }
                }
                else
                    return "Erro no Upload!";
            }
            catch { return "Erro no Upload"; }
        }

        public static string EnviarEmail(string emailDestinatario, string assunto, string corpomsg)
        {
            try
            {
                MailAddress de = new MailAddress("NewGen Tech <rudson.nn@gmail.com>");
                MailAddress para = new MailAddress(emailDestinatario);
                MailMessage mensagem = new MailMessage(de, para);
                mensagem.IsBodyHtml = true;
                mensagem.Subject = assunto;
                mensagem.Body = corpomsg;
                mensagem.Priority = MailPriority.Normal;
                SmtpClient cliente = new SmtpClient();
                cliente.Send(mensagem);
                return "success|E-mail enviado com sucesso";
            }
            catch { return "error|Erro ao enviar e-mail"; }
        }
        public static string Codifica(string texto)
        {
            byte[] stringBase64 = new byte[texto.Length];
            stringBase64 = Encoding.UTF8.GetBytes(texto);
            string codifica = Convert.ToBase64String(stringBase64);
            return codifica;
        }
        public static string Decodifica(string texto)
        {
            var encode = new UTF8Encoding();
            var utf8Decode = encode.GetDecoder();
            byte[] stringValor = Convert.FromBase64String(texto);
            int contador = utf8Decode.GetCharCount(stringValor, 0, stringValor.Length);
            char[] decodeChar = new char[contador];
            utf8Decode.GetChars(stringValor, 0, stringValor.Length, decodeChar, 0);
            string resultado = new String(decodeChar);
            return resultado;
        }

        public static string GerarGraficoPizza2(string titulo, string dados)
        {
            string grafico = @"<script src=""https://code.highcharts.com/highcharts.js""></script>
<script src=""https://code.highcharts.com/modules/exporting.js""></script>
<script src=""https://code.highcharts.com/modules/export-data.js""></script>
<script src=""https://code.highcharts.com/modules/accessibility.js""></script>
<figure class=""highcharts-figure"">
  <div id=""container""></div>
</figure>

<script type=""text/javascript"">
Highcharts.chart('container', {
  chart: {
    plotBackgroundColor: null,
    plotBorderWidth: null,
    plotShadow: false,
    type: 'pie'
  },
  title: {
    text: 'Quantidade total de OS por Status',
    align: 'center'
  },
  tooltip: {
    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
  },
  accessibility: {
    point: {
      valueSuffix: '%'
    }
  },
  plotOptions: {
    pie: {
      allowPointSelect: true,
      cursor: 'pointer',
      dataLabels: {
        enabled: true,
        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
      }
    }
  },

series: [
    {
      name: 'Status',
      colorByPoint: true,
      data: [" + dados + @"]
    }
  ]
});
</script>";
            return grafico;
        }

        public static string GerarGraficoColunas(string titulo, string dados1, string dados2, int ano)
        {
            string grafico = @"<script src=""https://code.highcharts.com/highcharts.js""></script>
<script src=""https://code.highcharts.com/modules/data.js""></script>
<script src=""https://code.highcharts.com/modules/drilldown.js""></script>
<script src=""https://code.highcharts.com/modules/exporting.js""></script>
<script src=""https://code.highcharts.com/modules/export-data.js""></script>
<script src=""https://code.highcharts.com/modules/accessibility.js""></script>

<figure class=""highcharts-figure"">
  <div id=""container3""></div>
</figure>

<script type=""text/javascript"">
Highcharts.chart('container3', {
  chart: {
    type: 'column'
  },
  title: {
    align: 'center',
    text: 'Gastos (R$) com Forncedores - Ano " + ano + @"'
  },
  subtitle: {
    align: 'center',
    text: 'Selecione o fornecedor para visualizar gastos mensais.'
  },
  accessibility: {
    announceNewData: {
      enabled: true
    }
},
  xAxis:
{
type: 'category'
  },
  yAxis:
{
title:
    {
    text: 'Valor em R$'
    }
},
  legend:
{
enabled: false
  },
  plotOptions:
{
series:
    {
    borderWidth: 0,
      dataLabels:
        {
        enabled: true,
        format: '{point.y:.1f}'
      }
    }
},

  tooltip:
{
headerFormat: '<span style=""font-size:11px"">{series.name}</span><br>',
    pointFormat: '<span style=""color:{point.color}"">{point.name}</span>: <b>{point.y:.2f}</b> reais<br/>'
  },

  series:
[
    {
name: 'Fornecedores',
      colorByPoint: true,
      data:
     [" + dados1 + @"]                                                                                                                              
    }
  ],
  drilldown:
{
breadcrumbs:
    {
    position:
        {
        align: 'left'
      }
    },
    series:
    [" + dados2 + @"]    
  }
});
</script>";
            return grafico;

        }

        public static string GerarGraficoBarras(string titulo, string dados1, string dados2)
        {
            string grafico = @"<script src=""https://code.highcharts.com/highcharts.js""></script>
<script src=""https://code.highcharts.com/modules/exporting.js""></script>
<script src=""https://code.highcharts.com/modules/export-data.js""></script>
<script src=""https://code.highcharts.com/modules/accessibility.js""></script>

<figure class=""highcharts-figure"">
  <div id=""container4""></div>
</figure>

<script type=""text/javascript"">
Highcharts.chart('container4', {
 chart: {
    type: 'bar'
  },
  title: {
    text: 'Gastos (R$) das OS por Tipo, classificados por CC',
    align: 'left'
  },
  xAxis: {
    categories: [" + dados1 + @"],
    title: {
      text: null
    },
    gridLineWidth: 1,
    lineWidth: 0
  },
  yAxis:
{
min: 0,
    title:
    {
    text: 'Valor em R$',
      align: 'high'
    },
    labels:
    {
    overflow: 'justify'
    },
    gridLineWidth: 0
  },
  tooltip:
{
valueSuffix: ' reais'
  },
  plotOptions:
{
bar:
    {
    borderRadius: '50%',
      dataLabels:
        {
        enabled: true
      },
      groupPadding: 0.1
    }
},
  legend:
{
layout: 'vertical',
    align: 'right',
    verticalAlign: 'top',
    x: -10,
    y: 50,
    floating: true,
    borderWidth: 1,
    backgroundColor:
    Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
    shadow: true
  },
  credits:
{
enabled: false
  },
  series:
[" + dados2 + @"]
});
</script>";
            return grafico;

        }

    }
}