using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    [Table("Produto")]
    public partial class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }
        public int? QuantidadeMinima { get; set; }
    }
}
