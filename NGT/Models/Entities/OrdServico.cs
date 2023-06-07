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

        [MaxLength(250)]
        public string DescProblema { get; set; }
        [MaxLength(250)]
        public string Obs { get; set; }
        [Required]
        public int FornecedorId { get; set; }
        public double Valor { get; set; }
        //public double Desconto { get; set; }
        [MaxLength(11)]
        public string NotaF { get; set; }
        [MaxLength(20)]
        public string CentroCusto { get; set; }
        [Required]
        public int ManutencaoTipoId { get; set; }
        public DateTime DataEntregaPrevis { get; set; }
        public DateTime? DataEntregaReal { get; set; }
        [Required]
        public int StatusTicketId { get; set; }
        public string NumTicketOS { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        [Required]
        public int UsuarioId { get; set; }


        public OrdServico()
        {
            DataCriacao = DateTime.Now;
        }
        //public int AnoAtualizacao
        //{
        //    get {
        //        if (DataAtualizacao.HasValue)
        //        {
        //            return DataAtualizacao.Value.Year;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //public virtual Ocorrencia Ocorrencia { get; set; }
        //public virtual Bloco Bloco { get; set; }
        //public virtual Local Local { get; set; }
        //public virtual Categoria Categoria { get; set; }
        //public virtual ItemDesc ItemDescs { get; set; }
        //public virtual Motivo Motivo { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual StatusTicket StatusTicket { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ManutencaoTipo ManutencaoTipo { get; set; }
        public virtual ICollection<OSItem> OSItem { get; set; }

    }
}