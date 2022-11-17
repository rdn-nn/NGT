using System.ComponentModel.DataAnnotations;

namespace NGT.Migrations
{
    using NGT.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Web.Razor.Parser.SyntaxTree;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.NgtContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data.NgtContexto cont)
        {

            cont.Status.AddOrUpdate(
                s => s.Nome,
                new Status { Id = 1, Nome = "Desativado" },
                new Status { Id = 2, Nome = "Ativado" }
            );

            cont.StatusTickets.AddOrUpdate(
                s => s.Nome,
                new StatusTicket { Id = 1, Nome = "Pendente" },
                new StatusTicket { Id = 2, Nome = "Em Andamento" },
                new StatusTicket { Id = 3, Nome = "Concluído" },
                new StatusTicket { Id = 4, Nome = "Cancelado" }
            );

            cont.Perfis.AddOrUpdate(
            p => p.Nome,
                new Perfil { Id = 1, Nome = "Administrador" },
                new Perfil { Id = 2, Nome = "Gerente" }
            );

            ////////Abc123
            cont.Usuarios.AddOrUpdate(
            u => u.Email,
                 new Usuario { Id = 1, Nome = "Nice Montarroyos", Email = "nice.montarroyos@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 },
                 new Usuario { Id = 2, Nome = "Jessica Aquiles", Email = "jessica.lucio@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 },
                 new Usuario { Id = 3, Nome = "Rudson Nunes", Email = "rudson.nunes@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 2, PerfilId = 1 },
                 new Usuario { Id = 3, Nome = "Wanderson dos Santos", Email = "wanderson.santos6@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 }
            );

            cont.Blocos.AddOrUpdate(
            b => b.Nome,
                new Bloco { Id = 1, Nome = "Bloco A", StatusId = 2 },
                new Bloco { Id = 2, Nome = "Bloco B", StatusId = 2 }
            );

            cont.Categorias.AddOrUpdate(
            c => c.Nome,
                new Categoria { Id = 1, Nome = "Mobília", StatusId = 2 },
                new Categoria { Id = 2, Nome = "Infraestrutura", StatusId = 2 }
            );

            cont.Motivos.AddOrUpdate(
            m => m.Nome,
                new Motivo { Id = 1, Nome = "Quebrado", StatusId = 2 },
                new Motivo { Id = 2, Nome = "Não funciona", StatusId = 2 }
            );

            cont.Locais.AddOrUpdate(
            l => l.Nome,
                new Local { Id = 1, Nome = "Sala 1", BlocoId = 1, StatusId = 2 },
                new Local { Id = 2, Nome = "Sala 2", BlocoId = 2, StatusId = 2 },
                new Local { Id = 3, Nome = "Sala 3", BlocoId = 1, StatusId = 2 }
            );

            cont.Itens.AddOrUpdate(
            i => i.Nome,
                new Item { Id = 1, Nome = "Cadeira", LocalId = 1, CategoriaId = 1, StatusId = 2 },
                new Item { Id = 2, Nome = "Mesa", LocalId = 1, CategoriaId = 1, StatusId = 2 },
                new Item { Id = 3, Nome = "Janela", LocalId = 2, CategoriaId = 2, StatusId = 2 }
            );


        }
    }
}
