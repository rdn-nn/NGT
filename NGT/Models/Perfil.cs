using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGT.Models
{
    public class Perfil
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }
        public int StatusId { get; set; }


        public virtual Status Status { get; set; }


        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}