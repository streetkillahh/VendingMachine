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
            var brands = context.Brands.ToList();

            var brandCocaCola = brands.FirstOrDefault(b => b.Name == "Coca-Cola");
            var brandSprite = brands.FirstOrDefault(b => b.Name == "Sprite");
            var brandFanta = brands.FirstOrDefault(b => b.Name == "Fanta");
            var brandDrPepper = brands.FirstOrDefault(b => b.Name == "Dr. Pepper");
            var brandPepsi = brands.FirstOrDefault(b => b.Name == "Pepsi");
            var brandMountainDew = brands.FirstOrDefault(b => b.Name == "Mountain Dew");
            var brandIrnBru = brands.FirstOrDefault(b => b.Name == "Irn Bru");
            var brandChernogolovka = brands.FirstOrDefault(b => b.Name == "Chernogolovka");


            context.Catalogs.AddRange(
                new Catalog 
                { 
                    Name = "Coca-Cola Classic",
                    Price = 50, 
                    Quantity = 10, 
                    Brand = brandCocaCola,
                    BrandId = brandCocaCola?.Id ?? 0 // подстраховка
                },
                new Catalog 
                { 
                    Name = "Sprite Lemon", 
                    Price = 45, 
                    Quantity = 8, 
                    Brand = brandSprite,
                    BrandId = brandSprite?.Id ?? 0 
                },
                new Catalog 
                { 
                    Name = "Fanta Orange", 
                    Price = 25, 
                    Quantity = 10, 
                    Brand = brandFanta,
                    BrandId = brandFanta?.Id ?? 0
                },
                new Catalog 
                { 
                    Name = "Dr. Pepper", 
                    Price = 45, 
                    Quantity = 10, 
                    Brand = brandDrPepper,
                    BrandId = brandDrPepper?.Id ?? 0 
                },
                new Catalog 
                { 
                    Name = "Pepsi Max", 
                    Price = 50, 
                    Quantity = 5, 
                    Brand = brandPepsi,
                    BrandId = brandPepsi?.Id ?? 0 
                },
                new Catalog 
                { 
                    Name = "Mountain Dew", 
                    Price = 48, 
                    Quantity = 7, 
                    Brand = brandMountainDew,
                    BrandId = brandMountainDew?.Id ?? 0 
                },
                new Catalog 
                { 
                    Name = "Irn Bru", 
                    Price = 40, 
                    Quantity = 12, 
                    Brand = brandIrnBru,
                    BrandId = brandIrnBru?.Id ?? 0 
                },
                new Catalog 
                { 
                    Name = "Chenogolovka", 
                    Price = 35, 
                    Quantity = 6, 
                    Brand = brandChernogolovka,
                    BrandId = brandChernogolovka?.Id ?? 0 
                }
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