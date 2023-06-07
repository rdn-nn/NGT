using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Motivo
    {
        public int Id { get; set; }
        [Required, MaxLength(250), Index(IsUnique = true)]
        public string Nome { get; set; }
        [Required]
        public int StatusId { get; set; }


        public virtual Status Status { get; set; }


        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
    }
}