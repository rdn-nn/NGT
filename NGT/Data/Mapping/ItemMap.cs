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
            Property(x => x.NumSerie).HasColumnName("ite_numserie");
            Property(x => x.Patrimonio).HasColumnName("ite_patrimonio");
            Property(x => x.HasPlaca).HasColumnName("ite_hasplaca");
            Property(x => x.IsPatInterno).HasColumnName("ite_ispatinterno");
            Property(x => x.LocalId).HasColumnName("loc_codigo");
            Property(x => x.CategoriaId).HasColumnName("cat_codigo");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}