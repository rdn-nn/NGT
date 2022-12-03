using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [Required, MaxLength(250), Index(IsUnique = true)]
        public string Nome { get; set; }
        [MaxLength(25)]
        public string NumSerie { get; set; }
        public int Patrimonio { get; set; }
        public bool HasPlaca { get; set; }
        public bool IsPatInterno { get; set; }
        [Required]
        public int LocalId { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public virtual Local Local { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
        public virtual ICollection<OrdServico> OrdServico { get; set; }
    }
}