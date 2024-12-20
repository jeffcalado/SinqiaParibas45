﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinqiaParibas45.Models
{
    public class Produto
    {
        [Key]
        public string COD_PRODUTO { get; set; } // PK
        public string DES_PRODUTO { get; set; }
        public string STA_STATUS { get; set; }

        // Relacionamento
        public ICollection<ProdutoCosif> ProdutoCosifs { get; set; }
    }


}