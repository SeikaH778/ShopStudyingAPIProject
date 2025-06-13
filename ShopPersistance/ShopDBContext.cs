using Microsoft.EntityFrameworkCore;
using ShopDomain.Models;
namespace ShopPersistance
{
    public class ShopDBContext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        public DbSet<SellerInfo> SellerInfo { get; set; }
        public ShopDBContext() { }
        public ShopDBContext(DbContextOptions<ShopDBContext> options)
       : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .ToTable("Products")
            .HasOne(p => p.Seller)          
            .WithMany(s => s.Products)      
            .HasForeignKey(p => p.SellerId);

            modelBuilder.Entity<CartItem>()
            .ToTable("CartItems")
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Buyer)
            .WithMany(b => b.Cart)
            .HasForeignKey(ci => ci.BuyerId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
