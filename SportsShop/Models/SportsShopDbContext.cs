using Microsoft.EntityFrameworkCore;

namespace SportsShop.Models
{
    public class SportsShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Cloth> Clothes { get; set; }

        public DbSet<Supplement> Supplements { get; set; }


        public SportsShopDbContext(DbContextOptions<SportsShopDbContext> options) : base(options) { }


    }
}
