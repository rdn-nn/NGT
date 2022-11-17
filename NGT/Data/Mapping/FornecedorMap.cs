using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NGT.Data.Mapping
{
    public class FornecedorMap : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMap()
        {
            ToTable("for_fornecedor");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("for_codigo");
            Property(x => x.RazaoSoc).HasColumnName("for_razaosoc");
            Property(x => x.NomeFantasia).HasColumnName("for_nomefantasia");
            Property(x => x.CNPJ).HasColumnName("for_cnpj");
            Property(x => x.IE).HasColumnName("for_ie");
            Property(x => x.Endereco).HasColumnName("for_endereco");
            Property(x => x.Telefone).HasColumnName("for_telefone");
            Property(x => x.Email).HasColumnName("for_email");
            Property(x => x.ServPrestado).HasColumnName("for_servprestado");
            Property(x => x.NomeResp).HasColumnName("for_responsavel");
            Property(x => x.CargoResp).HasColumnName("for_cargoresp");
            Property(x => x.RamoAtiv).HasColumnName("for_ramoativ");
            Property(x => x.Obs).HasColumnName("for_obs");
            Property(x => x.Id).HasColumnName("sta_codigo");
        }
    }
}
