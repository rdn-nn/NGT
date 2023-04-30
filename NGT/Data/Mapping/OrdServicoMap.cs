using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class OrdServicoMap : EntityTypeConfiguration<OrdServico>
    {
        public OrdServicoMap()
        {
            ToTable("ods_ordservico");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ods_codigo");
            Property(x => x.OcorrenciaId).HasColumnName("oco_codigo");
            Property(x => x.Patrimonio).HasColumnName("ods_patrimonio");
            Property(x => x.BlocoId).HasColumnName("blo_codigo");
            Property(x => x.LocalId).HasColumnName("loc_codigo");
            Property(x => x.CategoriaId).HasColumnName("cat_codigo");
            Property(x => x.ItemDescId).HasColumnName("itd_codigo");
            Property(x => x.MotivoId).HasColumnName("mot_codigo");
            Property(x => x.Obs).HasColumnName("ods_obs");
            Property(x => x.FornecedorId).HasColumnName("for_codigo");
            Property(x => x.NotaF).HasColumnName("ods_notafiscal");
            Property(x => x.CentroCusto).HasColumnName("ods_centrocusto");
            Property(x => x.Valor).HasColumnName("ods_valor");
            Property(x => x.Desconto).HasColumnName("ods_desconto");
            Property(x => x.DataEntregaPrevis).HasColumnName("ods_dataentregaprevista");
            Property(x => x.DataEntregaReal).HasColumnName("ods_dataentregareal");
            Property(x => x.DataCriacao).HasColumnName("ods_datacriacao");
            Property(x => x.DataAtualizacao).HasColumnName("ods_dataatualizacao");
            Property(x => x.NumTicketOS).HasColumnName("ods_numticketos");
            Property(x => x.StatusTicketId).HasColumnName("sti_codigo");
        }
    }
}
