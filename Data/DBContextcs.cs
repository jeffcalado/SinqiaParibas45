
using System.Data.Entity;

namespace SinqiaParibas45.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() : base("name=MinhaConexao")
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCosif> ProdutoCosifs { get; set; }
        public DbSet<MovimentoManual> MovimentosManuais { get; set; }


      

        //public AppDbContext(System.Data.Common.DbConnection connection)
        //    : base(connection, contextOwnsConnection: true) { }

      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Produto
            modelBuilder.Entity<Produto>()
                .ToTable("PRODUTO")
                .HasKey(e => e.COD_PRODUTO);

            modelBuilder.Entity<Produto>()
                .Property(e => e.COD_PRODUTO)
                .HasMaxLength(4)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(e => e.DES_PRODUTO)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(e => e.STA_STATUS)
                .HasMaxLength(1)
                .IsRequired();

            // ProdutoCosif
            modelBuilder.Entity<ProdutoCosif>()
                .ToTable("PRODUTO_COSIF")
                .HasKey(e => new { e.COD_PRODUTO, e.COD_COSIF });

            modelBuilder.Entity<ProdutoCosif>()
                .Property(e => e.COD_PRODUTO)
                .HasMaxLength(4)
                .IsRequired();

            modelBuilder.Entity<ProdutoCosif>()
                .Property(e => e.COD_COSIF)
                .HasMaxLength(11)
                .IsRequired();

            modelBuilder.Entity<ProdutoCosif>()
                .Property(e => e.COD_CLASSIFICACAO)
                .HasMaxLength(6)
                .IsRequired();

            modelBuilder.Entity<ProdutoCosif>()
                .Property(e => e.STA_STATUS)
                .HasMaxLength(1)
                .IsRequired();

            modelBuilder.Entity<ProdutoCosif>()
                .HasRequired(e => e.Produto)
                .WithMany(p => p.ProdutoCosifs)
                .HasForeignKey(e => e.COD_PRODUTO);

            // MovimentoManual
            modelBuilder.Entity<MovimentoManual>()
                .ToTable("MOVIMENTO_MANUAL")
                .HasKey(e => new { e.DAT_MES, e.DAT_ANO, e.NUM_LANCAMENTO });

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.DAT_MES)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.DAT_ANO)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.NUM_LANCAMENTO)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.COD_PRODUTO)
                .HasMaxLength(4)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.COD_COSIF)
                .HasMaxLength(11);

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.DES_DESCRICAO)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.DAT_MOVIMENTO)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.COD_USUARIO)
                .HasMaxLength(15);

            modelBuilder.Entity<MovimentoManual>()
                .Property(e => e.VAL_VALOR)
                .IsRequired();

            modelBuilder.Entity<MovimentoManual>()
                .HasRequired(e => e.ProdutoCosif)
                .WithMany(p => p.MovimentosManuais)
                .HasForeignKey(e => new { e.COD_PRODUTO, e.COD_COSIF });
        }
    }
}
