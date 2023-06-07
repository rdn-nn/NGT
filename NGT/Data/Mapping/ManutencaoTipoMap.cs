using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class ManutencaoTipoMap : EntityTypeConfiguration<ManutencaoTipo>
    {
        public ManutencaoTipoMap()
        {
            ToTable("mti_manutencaotipo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("mti_codigo");
            Property(x => x.Nome).HasColumnName("mti_nome");
            Property(x => x.StatusId).HasColumnName("sta_codigo");

        }
    }
}
