using Microsoft.AspNetCore.Mvc;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Complete(Dictionary<int, int>? change = null)
        {
            var viewModel = new PaymentResultViewModel
            {
                Change = change
            };

            return View("Success", viewModel);
        }

    }
}
