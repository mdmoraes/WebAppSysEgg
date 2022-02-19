using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAppSysEgg.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<EntradaProduto> EntradaProdutos { get; set; }
        public virtual DbSet<Estoque> Estoques { get; set; }
        public virtual DbSet<Fornecedor> Fornecedors { get; set; }
        public virtual DbSet<ItemPedido> ItemPedidos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=RYZEN\\SQLEXPRESS;Initial Catalog=SysEgg;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Endereco).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);
            });

            modelBuilder.Entity<EntradaProduto>(entity =>
            {
                entity.HasOne(d => d.Fornecedor)
                    .WithMany()
                    .HasForeignKey(d => d.FornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntradaProduto_Fornecedor");

                entity.HasOne(d => d.Produto)
                    .WithMany()
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntradaProduto_Produto");
            });

            modelBuilder.Entity<Estoque>(entity =>
            {
                entity.Property(e => e.DescricaoProduto).IsUnicode(false);
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.Property(e => e.FornecedorId).ValueGeneratedNever();

                entity.Property(e => e.FornecedorNome).IsUnicode(false);
            });

            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.Property(e => e.Item).IsUnicode(false);

                entity.HasOne(d => d.Pedido)
                    .WithMany()
                    .HasForeignKey(d => d.PedidoId)
                    .HasConstraintName("FK_ItemPedido_Pedidos");

                entity.HasOne(d => d.Produto)
                    .WithMany()
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemPedido_Produto");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Cliente");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.Descricao).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
