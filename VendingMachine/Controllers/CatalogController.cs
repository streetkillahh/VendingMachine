using Microsoft.AspNetCore.Mvc;
using VendingMachine.Models;
using VendingMachine.Models.Domain;


namespace VendingMachine.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Получаем список брендов для фильтра
            var brands = _context.Brands.ToList();

            // Передаем бренды через ViewBag
            ViewBag.Brands = brands;

            // Получаем список товаров для каталога
            var catalog = _context.Catalogs.ToList();

            // Передаем товары как модель представления
            return View(catalog);
        }
    }
}