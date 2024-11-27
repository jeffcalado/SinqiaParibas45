using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinqiaParibas45.Models
{
    public class ProdutoCosif
    {
        public string COD_PRODUTO { get; set; } // FK para Produto
        [Key]
        public string COD_COSIF { get; set; } // PK
        public string COD_CLASSIFICACAO { get; set; }
        public string STA_STATUS { get; set; }

        // Relacionamento
        public Produto Produto { get; set; }
        public ICollection<MovimentoManual> MovimentosManuais { get; set; }
    }
}
