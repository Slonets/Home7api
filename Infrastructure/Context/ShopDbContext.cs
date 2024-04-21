using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Basket> Orders { get; set; }
        public DbSet<ImagesUnit> Images { get; set; }
    }
}
