using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class OrdServico
    {

        public int Id { get; set; }
        public int OcorrenciaId { get; set; }
        public int Patrimonio { get; set; }
        [Required]
        public int BlocoId { get; set; }
        [Required]
        public int LocalId { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int ItemDescId { get; set; }
        [Required]
        public int MotivoId { get; set; }
        [MaxLength(250)]
        public string Obs { get; set; }
        [Required]
        public int FornecedorId { get; set; }
        [MaxLength(11)]
        public string NotaF { get; set; }
        [MaxLength(20)]
        public string CentroCusto { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }
        public DateTime DataEntregaPrevis { get; set; }
        public DateTime? DataEntregaReal { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string NumTicketOS { get; set; }
        [Required]
        public int StatusTicketId { get; set; }

        public OrdServico()
        {
            DataCriacao = DateTime.Now;
        }

        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual Bloco Bloco { get; set; }
        public virtual Local Local { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ItemDesc ItemDescs { get; set; }
        public virtual Motivo Motivo { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual StatusTicket StatusTicket { get; set; }

    }
}