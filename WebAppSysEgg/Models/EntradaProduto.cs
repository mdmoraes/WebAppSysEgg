using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    [Table("EntradaProduto")]
    public partial class EntradaProduto
    {
        [Key]
        [Required]  
        public int EntradaProdutoId { get; set;}
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEntrada { get; set; }
        public int FornecedorId { get; set; }

        [ForeignKey(nameof(FornecedorId))]
        public virtual Fornecedor Fornecedor { get; set; }
        [ForeignKey(nameof(ProdutoId))]
        public virtual Produto Produto { get; set; }
    }
}
