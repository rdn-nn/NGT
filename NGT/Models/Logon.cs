using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace NGT.Models
{
    public class Logon
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula,<br> minúscula e um número. Deve ter no mínimo 8 caracteres")]
        public string Senha { get; set; }
        public bool Perm { get; set; }
    }
}