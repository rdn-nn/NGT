using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class PerfilMap : EntityTypeConfiguration<Perfil>
    {
        public PerfilMap()
        {
            ToTable("per_perfil");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("per_codigo");
            Property(x => x.Nome).HasColumnName("per_nome");

        }
    }
}