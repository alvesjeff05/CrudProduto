using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CrudProduto.Models
{
    public partial class DbLojaSenaiContext : DbContext
    {
        public DbLojaSenaiContext()
        {
        }

        public DbLojaSenaiContext(DbContextOptions<DbLojaSenaiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Fornecedore> Fornecedores { get; set; } = null!;
        public virtual DbSet<Produto> Produtos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A10968A8466");

                entity.Property(e => e.Categoria1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("categoria");
            });

            modelBuilder.Entity<Fornecedore>(entity =>
            {
                entity.HasKey(e => e.IdFornecedores)
                    .HasName("PK__Forneced__F58F632FBE60884B");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__Produtos__2E883C23F4FDA6DA");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Produtos__IdCate__3A81B327");

                entity.HasOne(d => d.IdFornecedoresNavigation)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.IdFornecedores)
                    .HasConstraintName("FK__Produtos__IdForn__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
