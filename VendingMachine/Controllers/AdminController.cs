using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IO;
using VendingMachine.Models.Domain;

namespace VendingMachine.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Файл не выбран.";
                return View();
            }

            if (Path.GetExtension(file.FileName)?.ToLower() != ".xlsx")
            {
                ViewBag.Message = "Неверный формат файла. Поддерживаются только файлы .xlsx.";
                return View();
            }

            int importedCount = 0;

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                Log.Information($"Stream length: {stream.Length}");

                stream.Position = 0;

                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheets.FirstOrDefault();

                if (worksheet == null || worksheet.RangeUsed() == null)
                {
                    ViewBag.Message = "Лист Excel пуст или отсутствует.";
                    return View();
                }

                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Пропускаем заголовок
                foreach (var row in rows)
                {
                    try
                    {
                        var name = row.Cell(1).GetValue<string>()?.Trim();
                        var price = row.Cell(2).GetValue<decimal?>();
                        var quantity = row.Cell(3).GetValue<int?>();
                        var brandName = row.Cell(4).GetValue<string>()?.Trim();

                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(brandName) || !price.HasValue || !quantity.HasValue)
                            continue;

                        var brand = _context.Brands.FirstOrDefault(b => b.Name == brandName);
                        if (brand == null)
                        {
                            brand = new Brand { Name = brandName };
                            _context.Brands.Add(brand);
                            await _context.SaveChangesAsync();
                        }

                        var catalog = new Catalog
                        {
                            Name = name,
                            Price = price.Value,
                            Quantity = quantity.Value,
                            BrandId = brand.Id
                        };

                        _context.Catalogs.Add(catalog);
                        importedCount++;
                    }
                    catch (Exception exRow)
                    {
                        Log.Warning($"Ошибка при обработке строки: {exRow.Message}");
                        continue;
                    }
                }

                await _context.SaveChangesAsync();
                ViewBag.Message = $"Импортировано {importedCount} напитков.";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при импорте файла.");
                ViewBag.Message = "Ошибка при импорте. Подробнее в логах.";
            }

            return View();
        }
    }
}