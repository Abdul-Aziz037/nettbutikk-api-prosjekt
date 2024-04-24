using Microsoft.EntityFrameworkCore;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {

        }

        // tabellen lages
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
