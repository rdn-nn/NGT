using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Obs { get; set; }
        public string InfoAtualizacao { get; set; }
        public string Imagem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string NumTicket { get; set; }
        public string FotoOcorrencia { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int BlocoId { get; set; }
        [Required]
        public int LocalId { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int MotivoId { get; set; }
        [Required]
        public int StatusTicketId { get; set; }
        
        public Ocorrencia()
        {
            DataCriacao = DateTime.Now;
        }


        public virtual Bloco Bloco { get; set; }
        public virtual Local Local { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Item Item { get; set; }
        public virtual Motivo Motivo { get; set; }
        public virtual StatusTicket StatusTicket { get; set; }
        public virtual ICollection<OrdServico> OrdServico { get; set; }

    }
}