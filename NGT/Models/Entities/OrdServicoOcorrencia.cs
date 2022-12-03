using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class OrdServicoOcorrencia
    {
        public int Id { get; set; }
        [Required]
        public int OrdServicoId { get; set; }
        [Required]
        public int OcorrenciaId { get; set; }

        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual OrdServico OrdServico { get; set; }
    }
}