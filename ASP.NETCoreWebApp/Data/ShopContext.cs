using ASP.NETCoreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebApp.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { 
        }

        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Categori> Categoris { get; set; }
        public DbSet<DisCountCode> disCountCodes { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SubCategori> SubCategoris { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Attribute>().ToTable("Attribute");
            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Categori>().ToTable("Categori");
            modelBuilder.Entity<DisCountCode>().ToTable("DisCountCode");
            modelBuilder.Entity<Object>().ToTable("Object");
            modelBuilder.Entity<Sell>().ToTable("Sell");
            modelBuilder.Entity<Sell_Objec>().ToTable("Sell_Objec");
            modelBuilder.Entity<SubCategori>().ToTable("SubCategori");
            modelBuilder.Entity<Token>().ToTable("Token");
            modelBuilder.Entity<User>().ToTable("User");*/
        }
    }
}
