using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using SistemaVenda.Entities;

namespace SistemaVenda.DAL
{
    public partial class sistemavendasContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public sistemavendasContext(IConfiguration configuration)
        {
            _configuration = configuration;
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
                optionsBuilder.UseMySQL(_configuration.GetConnectionString("dbContext")) ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("categoria");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("cliente");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Celular)
                    .HasColumnName("celular")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CnpjCpf)
                    .HasColumnName("cnpj_cpf")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
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
                    .HasName("Fkcodcategoria_idx");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Codcategoria).HasColumnName("codcategoria");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Valor)
                    .HasColumnName("valor")
                    .HasColumnType("decimal(9,2)");

                entity.HasOne(d => d.CodcategoriaNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.Codcategoria)
                    .HasConstraintName("Fkcodcategoria");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
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
                    .HasName("fkcodcliente_idx");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Codcliente).HasColumnName("codcliente");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(9,2)");

                entity.HasOne(d => d.CodclienteNavigation)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.Codcliente)
                    .HasConstraintName("fkcodcliente");
            });

            modelBuilder.Entity<Vendaproduto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("vendaproduto");

                entity.HasIndex(e => e.Codigoproduto)
                    .HasName("fk_produto_idx");

                entity.HasIndex(e => e.Codigovenda)
                    .HasName("fk_vendas_idx");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Codigoproduto).HasColumnName("codigoproduto");

                entity.Property(e => e.Codigovenda).HasColumnName("codigovenda");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.CodigoprodutoNavigation)
                    .WithMany(p => p.Vendaproduto)
                    .HasForeignKey(d => d.Codigoproduto)
                    .HasConstraintName("fk_produto");

                entity.HasOne(d => d.CodigovendaNavigation)
                    .WithMany(p => p.Vendaproduto)
                    .HasForeignKey(d => d.Codigovenda)
                    .HasConstraintName("fk_vendas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
