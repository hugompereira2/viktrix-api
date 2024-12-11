using Microsoft.EntityFrameworkCore;
using viktrix_api.Models;

namespace viktrix_api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public required DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<ItemPedido>? ItensPedido { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().Property(p => p.Preco).IsRequired().HasPrecision(18, 2);

            modelBuilder.Entity<ItemPedido>().Property(i => i.Quantidade).IsRequired();

            modelBuilder.Entity<Pedido>().Ignore(p => p.Total);
        }
    }
}
