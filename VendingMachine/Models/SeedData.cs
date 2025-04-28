using VendingMachine.Models.Domain;

namespace VendingMachine.Models
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Brands.Any())
            {
                var brands = new List<Brand>
                {
                    new Brand { Name = "Coca-Cola" },
                    new Brand { Name = "Sprite" },
                    new Brand { Name = "Fanta" },
                    new Brand { Name = "Dr. Pepper" },
                    new Brand { Name = "Pepsi" },
                    new Brand { Name = "Mountain Dew" },
                    new Brand { Name = "Irn Bru" },
                    new Brand { Name = "Chernogolovka" }
                };

                context.Brands.AddRange(brands);
                context.SaveChanges();
            }

            if (!context.Catalogs.Any())
            {
                var brands = context.Brands.ToList();
                Catalog GetCatalog(string name, decimal price, int quantity, string brandName)
                {
                    var brand = brands.FirstOrDefault(b => b.Name == brandName)
                               ?? throw new Exception($"Бренд {brandName} не найден");

                    return new Catalog
                    {
                        Name = name,
                        Price = price,
                        Quantity = quantity,
                        BrandId = brand.Id
                    };
                }

                var catalogItems = new List<Catalog>
                {
                    GetCatalog("Coca-Cola Classic", 80.50m, 10, "Coca-Cola"),
                    GetCatalog("Coca-Cola Zero", 75.50m, 10, "Coca-Cola"),
                    GetCatalog("Sprite Lemon", 60.20m, 8, "Sprite"),
                    GetCatalog("Sprite Zero", 70.20m, 5, "Sprite"),
                    GetCatalog("Fanta Orange", 55.00m, 6, "Fanta"),
                    GetCatalog("Fanta Exotic", 95.10m, 4, "Fanta"),
                    GetCatalog("Dr. Pepper Original", 100.30m, 7, "Dr. Pepper"),
                    GetCatalog("Pepsi Max", 89.30m, 9, "Pepsi"),
                    GetCatalog("Pepsi Light", 72.20m, 5, "Pepsi"),
                    GetCatalog("Mountain Dew Citrus", 88.40m, 6, "Mountain Dew"),
                    GetCatalog("Irn Bru Original", 69.50m, 5, "Irn Bru"),
                    GetCatalog("Chernogolovka Baikal", 44.60m, 4, "Chernogolovka"),
                    GetCatalog("Chernogolovka Tarhun", 47.60m, 4, "Chernogolovka")
                };

                context.Catalogs.AddRange(catalogItems);
                context.SaveChanges();
            }

            if (!context.Coins.Any())
            {
                var coins = new List<Coin>
                {
                    new Coin { Denomination = 1, Quantity = 20 },
                    new Coin { Denomination = 2, Quantity = 20 },
                    new Coin { Denomination = 5, Quantity = 20},
                    new Coin { Denomination = 10, Quantity = 20}
                };

                context.Coins.AddRange(coins);
                context.SaveChanges();
            }
        }
    }
}
