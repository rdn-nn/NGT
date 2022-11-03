using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class StatusMap : EntityTypeConfiguration<Status>
    {
        public StatusMap(){
            ToTable("sta_status");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("sta_codigo");
            Property(x => x.Nome).HasColumnName("sta_nome");
        }
    }
}