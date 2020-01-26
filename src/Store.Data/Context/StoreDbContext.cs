using Microsoft.EntityFrameworkCore;
using Store.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Data.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);

            /* Outra opção seria aplicar a configuração para cada classe.
            modelBuilder.ApplyConfiguration(new Mappings.AddressMapping());
            modelBuilder.ApplyConfiguration(new Mappings.CategoryMapping());
            modelBuilder.ApplyConfiguration(new Mappings.ProductMapping());
            modelBuilder.ApplyConfiguration(new Mappings.SupplierMapping());
            */

            // Impede a exclusão em cascata.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
