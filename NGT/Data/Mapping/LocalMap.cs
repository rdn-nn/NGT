using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class LocalMap : EntityTypeConfiguration<Local>
    {
        public LocalMap()
        {
            ToTable("loc_local");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("loc_codigo");
            Property(x => x.Nome).HasColumnName("loc_nome");
            Property(x => x.BlocoId).HasColumnName("blo_codigo");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}