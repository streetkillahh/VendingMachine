using Microsoft.EntityFrameworkCore;
using VendingMachine.Models.Domain;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Игнорируем вычисляемое свойство TotalPrice
        modelBuilder.Entity<Order>()
            .Ignore(o => o.TotalPrice);
    }
}
