using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class OSItem
    {
        public int Id { get; set; }

        public int OrdServicoId { get; set; }
        public int ItemDescId { get; set; }

        public virtual OrdServico OrdServico { get; set; }
        public virtual ItemDesc ItemDesc { get; set; }
    }
}