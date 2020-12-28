using Microsoft.EntityFrameworkCore;
using ProvaML.Domain.Entities;

namespace ProvaML.Infrastructure.Data
{
    public class ProvaMLContext : DbContext
    {
        public ProvaMLContext(DbContextOptions<ProvaMLContext> options) : base(options)
        {
            
        }
        
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        }
        
    }
}