using System.Linq;
using VendingMachine.Models.Domain;
using VendingMachine.Models;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Добавление брендов
        if (!context.Brands.Any())
        {
            context.Brands.AddRange(
                new Brand { Name = "Coca-Cola" },
                new Brand { Name = "Pepsi" }
            );
            context.SaveChanges();
        }

        // Добавление товаров
        if (!context.Catalogs.Any())
        {
            var brandCocaCola = context.Brands.FirstOrDefault(b => b.Name == "Coca-Cola");
            var brandPepsi = context.Brands.FirstOrDefault(b => b.Name == "Pepsi");

            context.Catalogs.AddRange(
                new Catalog { Name = "Coca-Cola Classic", Price = 50, Quantity = 10, BrandId = brandCocaCola.Id },
                new Catalog { Name = "Pepsi Max", Price = 45, Quantity = 8, BrandId = brandPepsi.Id }
            );
            context.SaveChanges();
        }

        // Добавление монет
        if (!context.Coins.Any())
        {
            context.Coins.AddRange(
                new Coin { Denomination = 1, Quantity = 50 },
                new Coin { Denomination = 2, Quantity = 50 },
                new Coin { Denomination = 5, Quantity = 50 },
                new Coin { Denomination = 10, Quantity = 50 }
            );
            context.SaveChanges();
        }
    }
}