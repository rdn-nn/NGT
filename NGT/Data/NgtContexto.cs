using NGT.Data.Mapping;
using NGT.Models.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NGT.Data
{
    public class NgtContexto : DbContext
    {
        public NgtContexto() : base("ConexaoBD")
        { }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Status> Statuses { get; set; }
        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            mb.Configurations.Add(new PerfilMap());
            mb.Configurations.Add(new UsuarioMap());
            mb.Configurations.Add(new StatusMap());

            //mb.Configurations.Add(new BlocoMap());
            //mb.Configurations.Add(new CategoriaMap());
            //mb.Configurations.Add(new LocalMap());
            //mb.Configurations.Add(new ItemMap());
            //mb.Configurations.Add(new OcorrenciaMap());
            base.OnModelCreating(mb);
        }
    }
}