using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    public partial class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataPedido { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPedido { get; set; }

        [ForeignKey(nameof(ClienteId))]
        [InverseProperty("Pedidos")]
        public virtual Cliente Cliente { get; set; }
    }
}
