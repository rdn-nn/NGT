using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {
            ToTable("cat_categoria");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("cat_codigo");
            Property(x => x.Nome).HasColumnName("cat_nome");
            Property(x => x.StatusId).HasColumnName("sta_codigo");
        }
    }
}