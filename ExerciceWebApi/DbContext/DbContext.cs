using ExerciceWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExerciceWebApi
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<InputOutput> InputOutputs { get; set; }
		public DbSet<Storage> Storages { get; set; }
		public DbSet<Warehouse> Warehouses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Category>(category =>
			{
				category.ToTable("Category");

				category.HasKey(c => c.CategoryId);
				category.Property(c => c.CategoryName).IsRequired(false).HasMaxLength(100);
				category.HasMany(c => c.Products);

			});

			modelBuilder.Entity<Product>(product =>
			{
				product.ToTable("Product");

				product.HasKey(c => c.ProductId);
				product.Property(c => c.ProductName).IsRequired(false).HasMaxLength(100);
				product.Property(c => c.ProductDescription).IsRequired(false).HasMaxLength(500);
				product.Property(c => c.TotalQuantity).IsRequired(false);
				product.Property(c=> c.CategoryId);
				product.HasOne(c=> c.Category);
				product.HasMany(c => c.Storages);

			});
		}
    }
}