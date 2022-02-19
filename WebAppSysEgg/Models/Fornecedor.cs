using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAppSysEgg.Models
{
    [Table("Fornecedor")]
    public partial class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }
        [Required]
        [StringLength(100)]
        public string FornecedorNome { get; set; }
    }
}
