using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
