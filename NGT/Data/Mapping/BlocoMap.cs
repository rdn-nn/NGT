using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class BlocoMap:EntityTypeConfiguration<Bloco>
    {
        public BlocoMap()
        {
            ToTable("blo_bloco");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("blo_codigo");
            Property(x => x.Nome).HasColumnName("blo_nome");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}