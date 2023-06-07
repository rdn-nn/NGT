using System.ComponentModel.DataAnnotations;

namespace NGT.Migrations
{
    using NGT.Models;
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
                new Perfil { Id = 1, Nome = "Diretor de Serviços" },
                new Perfil { Id = 2, Nome = "Administrador" }
            );

            ////////Abcd123*
            cont.Usuarios.AddOrUpdate(
            u => u.Email,
                 new Usuario { Id = 1, Nome = "Nice Montarroyos", Email = "nice.montarroyos@fatec.sp.gov.br", Senha = "641IWbeutP6fjoNs5swXN32h1bvBNz5tSCUNZe549ysNptjgB2Pz+dBPZ/D0GHrDkwOP3c4QCmjmc+UeWyk1TA==", StatusId = 1, PerfilId = 2 , FotoPerfil = "\\Areas\\Admin\\Content\\Images\\anonimo.jpg" },
                 new Usuario { Id = 2, Nome = "Jessica Aquiles", Email = "jessica.lucio@fatec.sp.gov.br", Senha = "641IWbeutP6fjoNs5swXN32h1bvBNz5tSCUNZe549ysNptjgB2Pz+dBPZ/D0GHrDkwOP3c4QCmjmc+UeWyk1TA==", StatusId = 1, PerfilId = 2, FotoPerfil = "\\Areas\\Admin\\Content\\Images\\anonimo.jpg" },
                 new Usuario { Id = 3, Nome = "Rudson Nunes", Email = "rudson.nunes@fatec.sp.gov.br", Senha = "641IWbeutP6fjoNs5swXN32h1bvBNz5tSCUNZe549ysNptjgB2Pz+dBPZ/D0GHrDkwOP3c4QCmjmc+UeWyk1TA==", StatusId = 2, PerfilId = 1, FotoPerfil = "\\Areas\\Admin\\Content\\Images\\anonimo.jpg" },
                 new Usuario { Id = 3, Nome = "Wanderson dos Santos", Email = "wanderson.santos6@fatec.sp.gov.br", Senha = "641IWbeutP6fjoNs5swXN32h1bvBNz5tSCUNZe549ysNptjgB2Pz+dBPZ/D0GHrDkwOP3c4QCmjmc+UeWyk1TA==", StatusId = 1, PerfilId = 2, FotoPerfil = "\\Areas\\Admin\\Content\\Images\\anonimo.jpg" }
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
                new Item { Id = 1, Nome = "Cadeira", StatusId = 2 },
                new Item { Id = 2, Nome = "Mesa", StatusId = 2 },
                new Item { Id = 3, Nome = "Janela", StatusId = 2 }
            );

            cont.ItemDescs.AddOrUpdate(
            it => it.Id,
                new ItemDesc { Id = 1, ItemId = 1, LocalId = 1, CategoriaId = 1, StatusId = 2 },
                new ItemDesc { Id = 2, ItemId = 2, LocalId = 1, CategoriaId = 1, StatusId = 2 },
                new ItemDesc { Id = 3, ItemId = 3, LocalId = 2, CategoriaId = 2, StatusId = 2 }
            );


        }
    }
}
