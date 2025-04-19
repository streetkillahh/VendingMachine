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
                new Brand { Name = "Sprite" },
                new Brand { Name = "Fanta"},
                new Brand { Name = "Dr. Pepper"},
                new Brand { Name = "Pepsi"},
                new Brand { Name = "Mountain Dew"},
                new Brand { Name = "Irn Bru"},
                new Brand { Name = "Черноголовка"}
            );
            context.SaveChanges();
        }

        // Добавление товаров
        if (!context.Catalogs.Any())
        {
            var brandCocaCola = context.Brands.FirstOrDefault(b => b.Name == "Coca-Cola");
            var brandSprite = context.Brands.FirstOrDefault(b => b.Name == "Sprite");
            var brandFanta = context.Brands.FirstOrDefault(b => b.Name == "Fanta");
            var brandDrPepper = context.Brands.FirstOrDefault(b => b.Name == "Dr. Pepper");
            var brandPepsi = context.Brands.FirstOrDefault(b => b.Name == "Pepsi");
            var brandMountainDew = context.Brands.FirstOrDefault(b => b.Name == "Mountain Dew");
            var brandIrnBru = context.Brands.FirstOrDefault(b => b.Name == "Irn Bru");
            var brandChernogolovka = context.Brands.FirstOrDefault(b => b.Name == "Черноголовка");


            context.Catalogs.AddRange(
                new Catalog { Name = "Coca-Cola Classic", Price = 50, Quantity = 10, BrandId = brandCocaCola.Id },
                new Catalog { Name = "Sprite Lemon", Price = 45, Quantity = 8, BrandId = brandSprite.Id },
                new Catalog { Name = "Fanta Orange", Price = 25, Quantity = 10, BrandId = brandFanta.Id },
                new Catalog { Name = "Dr. Pepper", Price = 45, Quantity = 10, BrandId = brandDrPepper.Id },
                new Catalog { Name = "Pepsi Max", Price = 50, Quantity = 5, BrandId = brandPepsi.Id },
                new Catalog { Name = "Mountain Dew", Price = 48, Quantity = 7, BrandId = brandMountainDew.Id },
                new Catalog { Name = "Irn Bru", Price = 40, Quantity = 12, BrandId = brandIrnBru.Id },
                new Catalog { Name = "Черноголовка", Price = 35, Quantity = 6, BrandId = brandChernogolovka.Id }
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