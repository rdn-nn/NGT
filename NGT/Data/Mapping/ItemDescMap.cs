using NGT.Models;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class ItemDescMap : EntityTypeConfiguration<ItemDesc>
    {
        public ItemDescMap() {
            ToTable("itd_itemdesc");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("itd_codigo");
            Property(x => x.ItemId).HasColumnName("ite_codigo");
            Property(x => x.NumSerie).HasColumnName("itd_numserie");
            Property(x => x.Patrimonio).HasColumnName("itd_patrimonio");
            Property(x => x.HasPlaca).HasColumnName("itd_hasplaca");
            Property(x => x.IsPatInterno).HasColumnName("itd_ispatinterno");
            Property(x => x.LocalId).HasColumnName("loc_codigo");
            Property(x => x.CategoriaId).HasColumnName("cat_codigo");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}
