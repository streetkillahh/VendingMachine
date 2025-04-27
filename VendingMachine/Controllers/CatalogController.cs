using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services;
using System.Threading.Tasks;

namespace VendingMachine.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var catalogItems = await _catalogService.GetAllCatalogItemsAsync();
            return View(catalogItems);
        }
    }
}
