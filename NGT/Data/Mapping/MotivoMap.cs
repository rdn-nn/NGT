using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class MotivoMap : EntityTypeConfiguration<Motivo>
    {
        public MotivoMap()
        {
            ToTable("mot_motivo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("mot_codigo");
            Property(x => x.Nome).HasColumnName("mot_nome");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}