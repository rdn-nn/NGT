using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Bloco> Bloco { get; set; }
        public virtual ICollection<Categoria> Categoria { get; set; }
        public virtual ICollection<Motivo> Motivo { get; set; }
        public virtual ICollection<Local> Local { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual ICollection<ItemDesc> ItemDescs { get; set; }
        public virtual ICollection<Fornecedor> Fornecedor { get; set; }
    }
}