using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class OcorrenciaMap : EntityTypeConfiguration<Ocorrencia>
    {
        public OcorrenciaMap()
        {
            ToTable("oco_ocorrencia");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("oco_codigo");
            Property(x => x.Obs).HasColumnName("oco_obs");
            Property(x => x.Imagem).HasColumnName("oco_img");
            Property(x => x.DataCriacao).HasColumnName("oco_datacriacao");
            Property(x => x.DataAtualizacao).HasColumnName("oco_dataatualizacao");
            Property(x => x.NumTicket).HasColumnName("oco_numticket");
            Property(x => x.BlocoId).HasColumnName("blo_codigo");
            Property(x => x.LocalId).HasColumnName("loc_codigo");
            Property(x => x.CategoriaId).HasColumnName("cat_codigo");
            Property(x => x.ItemId).HasColumnName("ite_codigo");
            Property(x => x.MotivoId).HasColumnName("mot_codigo");
            Property(x => x.StatusTicketId).HasColumnName("sti_codigo");
        }

    }
}