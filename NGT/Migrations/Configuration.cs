namespace NGT.Migrations
{
    using NGT.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Web.Razor.Parser.SyntaxTree;

    internal sealed class Configuration : DbMigrationsConfiguration<NGT.Data.NgtContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NGT.Data.NgtContexto cont)
        {
            //cont.Blocos.AddOrUpdate(
            //b => b.Nome,
            //    new Models.Entities.Bloco { Nome = "Bloco A", Status = Bloco.TipoBlo.ATIVO },
            //    new Models.Entities.Bloco { Nome = "Bloco B", Status = Bloco.TipoBlo.ATIVO }
            //);
            //cont.Categorias.AddOrUpdate(
            //    c => c.Nome,
            //    new Models.Entities.Categoria { Nome = "Mobília", Status = Categoria.TipoCat.ATIVO },
            //    new Models.Entities.Categoria { Nome = "Infraestrutura", Status = Categoria.TipoCat.ATIVO }
            //);
            //cont.Locais.AddOrUpdate(
            //l => l.Nome,
            //new Models.Entities.Local { Nome = "Sala 1", Bloco = context.Blocos.Find(1), Status = Local.TipoLoc.ATIVO },
            //new Models.Entities.Local { Nome = "Sala 2", Bloco = context.Blocos.Find(2), Status = Local.TipoLoc.ATIVO },
            //    new Models.Entities.Local { Nome = "Sala 3", Bloco = context.Blocos.Find(1), Status = Local.TipoLoc.ATIVO }
            //);
            //cont.Itens.AddOrUpdate(
            //i => i.Nome,
            //new Models.Entities.Item { Nome = "Cadeira", TemPlaca = Item.Tipo.NÃO, PatInterno = Item.Tipo.NÃO, Local = context.Locais.Find(1), Categoria = context.Categorias.Find(1), Status = Item.TipoIte.ATIVO },
            //new Models.Entities.Item { Nome = "Mesa", TemPlaca = Item.Tipo.SIM, PatInterno = Item.Tipo.SIM, Local = context.Locais.Find(2), Categoria = context.Categorias.Find(1), Status = Item.TipoIte.ATIVO },
            //new Models.Entities.Item { Nome = "Janela", TemPlaca = Item.Tipo.NÃO, PatInterno = Item.Tipo.SIM, Local = context.Locais.Find(3), Categoria = context.Categorias.Find(2), Status = Item.TipoIte.ATIVO }
            //);

            cont.Statuses.AddOrUpdate(
                s => s.Nome,
                new Status { Nome = "Desativado" },
                new Status { Nome = "Ativado" }
            );

            cont.Perfis.AddOrUpdate(
            p => p.Nome,
                new Perfil { Nome = "Administrador"  },
                new Perfil { Nome = "Gerente" }
            );

            ////Abc123
            cont.Usuarios.AddOrUpdate(
            u => u.Email,
                 new Usuario { Nome = "Nice Montarroyos", Email = "nice.montarroyos@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 },
                 new Usuario { Nome = "Jessica Aquiles", Email = "jessica.lucio@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 },
                 new Usuario { Nome = "Rudson Nunes", Email = "rudson.nunes@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 2, PerfilId = 1 },
                 new Usuario { Nome = "Wanderson dos Santos", Email = "wanderson.santos6@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 }
            );
        }
    }
}
