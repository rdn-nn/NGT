using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            ToTable("ite_item");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ite_codigo");
            Property(x => x.Nome).HasColumnName("ite_nome");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}