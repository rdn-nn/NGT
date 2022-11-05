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
                new Status { Nome = "Desativado" },
                new Status { Nome = "Ativado" }
            );

            cont.StatusTickets.AddOrUpdate(
                s => s.Nome,
                new StatusTicket { Nome = "Pendente" },
                new StatusTicket { Nome = "Em Andamento" },
                new StatusTicket { Nome = "Concluído" },
                new StatusTicket { Nome = "Cancelado" }
            );

            cont.Perfis.AddOrUpdate(
            p => p.Nome,
                new Perfil { Nome = "Administrador" },
                new Perfil { Nome = "Gerente" }
            );

            ////////Abc123
            cont.Usuarios.AddOrUpdate(
            u => u.Email,
                 new Usuario { Nome = "Nice Montarroyos", Email = "nice.montarroyos@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 },
                 new Usuario { Nome = "Jessica Aquiles", Email = "jessica.lucio@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 },
                 new Usuario { Nome = "Rudson Nunes", Email = "rudson.nunes@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 2, PerfilId = 1 },
                 new Usuario { Nome = "Wanderson dos Santos", Email = "wanderson.santos6@fatec.sp.gov.br", Senha = "vDDsx1jGNpHGnmbYRjJmcJJL/5YJtf6/OcHobMqPtyeDrV5bcHY1nm1wm8WM03mt4UlZRfhZHph2yyY05DE5pg==", StatusId = 1, PerfilId = 2 }
            );

            cont.Blocos.AddOrUpdate(
            b => b.Nome,
                new Bloco { Nome = "Bloco A", StatusId = 2 },
                new Bloco { Nome = "Bloco B", StatusId = 2 }
            );

            cont.Categorias.AddOrUpdate(
            c => c.Nome,
                new Categoria { Nome = "mobília", StatusId = 2 },
                new Categoria { Nome = "infraestrutura", StatusId = 2 }
            );

            cont.Motivos.AddOrUpdate(
            m => m.Nome,
                new Motivo { Nome = "Quebrado", StatusId = 2 },
                new Motivo { Nome = "Não funciona", StatusId = 2 }
            );

            cont.Locais.AddOrUpdate(
            l => l.Nome,
                new Local { Nome = "Sala 1", BlocoId = 1, StatusId = 2 },
                new Local { Nome = "Sala 2", BlocoId = 2, StatusId = 2 },
                new Local { Nome = "Sala 3", BlocoId = 1, StatusId = 2 }
            );

            cont.Itens.AddOrUpdate(
            i => i.Nome,
                new Item { Nome = "Cadeira", LocalId = 1, CategoriaId = 1, StatusId = 2 },
                new Item { Nome = "Mesa", LocalId = 1, CategoriaId = 1, StatusId = 2 },
                new Item { Nome = "Janela", LocalId = 2, CategoriaId = 2, StatusId = 2 }
            );


        }
    }
}
