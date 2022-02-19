using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    [Table("ItemPedido")]
    public partial class ItemPedido
    {
        [Key]
        [Required]
        public int ItemPedidoId { get; set; }   
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(50)]
        public string Item { get; set; }
        public int Quantidade { get; set; }
        [Column(TypeName = "money")]
        public decimal ValorUnitario { get; set; }
        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [ForeignKey(nameof(PedidoId))]
        public virtual Pedido Pedido { get; set; }
        [ForeignKey(nameof(ProdutoId))]
        public virtual Produto Produto { get; set; }
    }
}
