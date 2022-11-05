using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class StatusTicketMap : EntityTypeConfiguration<StatusTicket>
    {
        public StatusTicketMap()
        {
            ToTable("sti_statusticket");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("sti_codigo");
            Property(x => x.Nome).HasColumnName("sti_nome");
        }
    }
}