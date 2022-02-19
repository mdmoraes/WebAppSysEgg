using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        [Key]
        public int ClienteId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(150)]
        public string Endereco { get; set; }

        [InverseProperty(nameof(Pedido.Cliente))]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
