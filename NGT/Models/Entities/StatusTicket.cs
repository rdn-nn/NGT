using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class StatusTicket
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
        public virtual ICollection<OrdServico> OrdServico { get; set; }
    }
}