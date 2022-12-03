using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Perfil
    {
        public int Id { get; set; }

        [Required, MaxLength(100), Index(IsUnique = true)]
        public string Nome { get; set; }
       
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}