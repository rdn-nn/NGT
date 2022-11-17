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

        public DbSet<Status> Status { get; set; }
        public DbSet<StatusTicket> StatusTickets { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Bloco> Blocos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Motivo> Motivos { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<OrdServico> OrdServicos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            mb.Configurations.Add(new StatusMap());
            mb.Configurations.Add(new StatusTicketMap());
            mb.Configurations.Add(new PerfilMap());
            mb.Configurations.Add(new UsuarioMap());
            mb.Configurations.Add(new BlocoMap());
            mb.Configurations.Add(new CategoriaMap());
            mb.Configurations.Add(new MotivoMap());
            mb.Configurations.Add(new LocalMap());
            mb.Configurations.Add(new ItemMap());
            mb.Configurations.Add(new OcorrenciaMap());
            mb.Configurations.Add(new OrdServicoMap());
            mb.Configurations.Add(new FornecedorMap());
            base.OnModelCreating(mb);
        }
    }
}