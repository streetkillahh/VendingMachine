using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
