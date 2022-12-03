using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Nome { get; set; }

        [Required, MaxLength(100), EmailAddress, Index(IsUnique = true)]
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Hash { get; set; }
        public string FotoPerfil { get; set; }
        [Required]
        public int PerfilId { get; set; }
        [Required]
        public int StatusId { get; set; }



        public virtual Perfil Perfil { get; set; }
        public virtual Status Status { get; set; }
        
    }
}