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
            char[] chars = new char[18];
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
                double permitido = 1800;
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
                double permitido = 2700;
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
    }
}