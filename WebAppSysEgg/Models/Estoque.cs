using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    [Keyless]
    [Table("Estoque")]
    public partial class Estoque
    {
        public int? ProdutoId { get; set; }
        [StringLength(150)]
        public string DescricaoProduto { get; set; }
        public int? Quantidade { get; set; }
    }
}
