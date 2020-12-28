using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaML.Domain.Entities;

namespace ProvaML.Infrastructure.Data
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Nome).IsRequired();
            builder.Property(u => u.ValorVenda).HasColumnType("decimal(15,2)").IsRequired();
            builder.Property(u => u.Imagem);
        }
    }
}