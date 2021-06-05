using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaVenda.Entities;

namespace SistemaVenda.DAL
{
    public partial class sistemavendasContext : DbContext
    {
        public sistemavendasContext()
        {
        }

        public sistemavendasContext(DbContextOptions<sistemavendasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<Vendaproduto> Vendaproduto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=sistemavendas");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("categoria");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("cliente");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CnpjCpf)
                    .IsRequired()
                    .HasColumnName("cnpj_cpf")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("produto");

                entity.HasIndex(e => e.Codcategoria)
                    .HasName("codcategori_idx");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Codcategoria)
                    .HasColumnName("codcategoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Quantidade)
                    .HasColumnName("quantidade")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Valor)
                    .HasColumnName("valor")
                    .HasColumnType("decimal(9,2)");

                entity.HasOne(d => d.CodcategoriaNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.Codcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("codcategori");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnName("senha")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("venda");

                entity.HasIndex(e => e.Codcliente)
                    .HasName("fkclient_idx");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Codcliente)
                    .HasColumnName("codcliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(9,2)");

                entity.HasOne(d => d.CodclienteNavigation)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.Codcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkclient");
            });

            modelBuilder.Entity<Vendaproduto>(entity =>
            {
                entity.HasKey(e => new { e.Codigovenda, e.Codigoproduto })
                    .HasName("PRIMARY");

                entity.ToTable("vendaproduto");

                entity.HasIndex(e => e.Codigoproduto)
                    .HasName("fkcodproduto_idx");

                entity.Property(e => e.Codigovenda)
                    .HasColumnName("codigovenda")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Codigoproduto)
                    .HasColumnName("codigoproduto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(9,2)");

                entity.Property(e => e.Valorunitario)
                    .HasColumnName("valorunitario")
                    .HasColumnType("decimal(9,2)");

                entity.HasOne(d => d.CodigoprodutoNavigation)
                    .WithMany(p => p.Vendaproduto)
                    .HasForeignKey(d => d.Codigoproduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkcodproduto");

                entity.HasOne(d => d.CodigovendaNavigation)
                    .WithMany(p => p.Vendaproduto)
                    .HasForeignKey(d => d.Codigovenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkcodvenda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
