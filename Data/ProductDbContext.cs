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
        public DbSet<Product> products { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
