using NGT.Data.Mapping;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class OSItemMap : EntityTypeConfiguration<OSItem>
    {
        public OSItemMap()
        {
            ToTable("osi_ositem");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("osi_codigo");
            Property(x => x.OrdServicoId).HasColumnName("ods_codigo");
            Property(x => x.ItemDescId).HasColumnName("itd_codigo");
        }
    }
}
