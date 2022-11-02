using NGT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NGT.Data
{
    public class NgtContexto : DbContext
    {
        public NgtContexto() : base("ConexaoBD")
        { }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}