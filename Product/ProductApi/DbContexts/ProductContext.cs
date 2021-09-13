using Microsoft.EntityFrameworkCore;
using ProductApi.Model;

namespace ProductApi.DbContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics"
                },
                new Category
                {
                    Id = 2,
                    Name = "Computer"
                },
                new Category
                {
                    Id = 3,
                    Name = "Clothings"
                },
                new Category
                {
                    Id = 4,
                    Name = "Grocery"
                });
        }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
    }
}
