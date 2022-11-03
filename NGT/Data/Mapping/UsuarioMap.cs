using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class UsuarioMap:EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("usu_usuario");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("usu_codigo");
            Property(x => x.Nome).HasColumnName("usu_nome");
            Property(x => x.Email).HasColumnName("usu_email");
            Property(x => x.Senha).HasColumnName("usu_senha");
            Property(x => x.Hash).HasColumnName("usu_hash");
            Property(x => x.FotoPerfil).HasColumnName("usu_imgperfil");
            Property(x => x.PerfilId).HasColumnName("per_codigo");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}
