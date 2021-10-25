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
        public DbSet<Sell_Objec> Sells_Objecs { get; set; }
        public DbSet<SubCategori> SubCategoris { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
