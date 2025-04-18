using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
