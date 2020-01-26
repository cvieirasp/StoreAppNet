using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Business.Models;

namespace Store.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.CEP)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(s => s.PublicPlace)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(s => s.Number)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(s => s.Complement)
                .HasColumnType("varchar(250)");

            builder.Property(s => s.District)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(s => s.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(s => s.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Addresses");
        }
    }
}