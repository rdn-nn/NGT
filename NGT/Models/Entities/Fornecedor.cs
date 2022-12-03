using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGT.Models.Entities
{
    public class Fornecedor
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string RazaoSoc { get; set; }
        public string NomeFantasia { get; set; }
        [Required]
        public long CNPJ { get; set; }
        [Required]
        public long IE { get; set; }
        [Required]
        [MaxLength(250)]
        public string Endereco { get; set; }
        [MaxLength(250)]
        public string Telefone { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string ServPrestado { get; set; }
        [Required]
        [MaxLength(150)]
        public string NomeResp { get; set; }
        [MaxLength(100)]
        public string CargoResp { get; set; }
        [MaxLength(100)]
        public string RamoAtiv { get; set; }
        [MaxLength(250)]
        public string Obs { get; set; }
        [Required]
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<OrdServico> OrdServico { get; set; }
    }
}
