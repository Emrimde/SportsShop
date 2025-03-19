using Microsoft.EntityFrameworkCore;

namespace SportsShop.Models
{
    public class SportsShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .Property(c => c.Price)
                .HasPrecision(18, 2); 
        }













        public SportsShopDbContext(DbContextOptions<SportsShopDbContext> options) : base(options) { }


    }
}
