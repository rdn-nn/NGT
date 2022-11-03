﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}