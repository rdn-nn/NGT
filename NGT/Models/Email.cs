using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NGT.Models
{
    public class Email
    {
        [EmailAddress]
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Assunto { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Mensagem")]
        public string CorpoMsg { get; set; }
    }
}