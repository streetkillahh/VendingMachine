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
        // Настройка точности и масштаба для свойства Price в таблице Catalog
        modelBuilder.Entity<Catalog>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

        // Настройка точности и масштаба для свойства TotalAmount в таблице Order
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,2)");


        // Настройка точности для свойства UnitPrice в таблице OrderItem
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2");
    }
}