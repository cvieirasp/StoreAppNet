using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Business.Models;

namespace Store.Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(s => s.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(s => s.SupplierType)
                .HasColumnType("tinyint");

            // 1 : 1 => Fornecedor : Endereço
            builder.HasOne(s => s.Address)
                .WithOne(a => a.Supplier);

            // 1 : N => Fornecedor : Produtos
            builder.HasMany(s => s.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            builder.ToTable("Suppliers");
        }
    }
}
