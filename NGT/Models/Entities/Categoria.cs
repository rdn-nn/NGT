using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Nome { get; set; }
        [Required]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
        public virtual ICollection<OrdServico> OrdServico { get; set; }
    }
}