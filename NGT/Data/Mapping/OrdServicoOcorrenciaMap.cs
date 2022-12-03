using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class OrdServicoOcorrenciaMap : EntityTypeConfiguration<OrdServicoOcorrencia>
    {
        public OrdServicoOcorrenciaMap()
        {
            ToTable("oso_ordservicoocorrencia");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("oso_codigo");
            Property(x => x.OrdServicoId).HasColumnName("ods_codigo");
            Property(x => x.OcorrenciaId).HasColumnName("oco_codigo");

        }
    }
}