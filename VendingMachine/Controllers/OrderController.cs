using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services;
using VendingMachine.Repositories;
using VendingMachine.Models.Domain;

namespace VendingMachine.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogRepository _catalogRepository;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository, ICatalogRepository catalogRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _catalogRepository = catalogRepository;
        }

        /* -----> Trouble with selectedCatalogIds <-----
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] List<int> selectedCatalogIds)
        {
            if (selectedCatalogIds == null || !selectedCatalogIds.Any())
            {
                return BadRequest("Ничего не выбрано.");
            }

            await _orderService.CreateOrderAsync(selectedCatalogIds);
            return Ok();
        }
        */


        /* -----> Update this <-----
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(decimal paidAmount)
        {
            var order = await _orderRepository.GetLastUnpaidOrderAsync();
            if (order == null)
            {
                return RedirectToAction("Checkout");
            }

            var totalPrice = order.Items.Sum(i => i.Quantity * (i.Catalog?.Price ?? 0));

            if (paidAmount < totalPrice)
            {
                TempData["PaymentError"] = "Недостаточно средств для оплаты заказа.";
                return RedirectToAction("Payment");
            }

            order.IsPaid = true;
            await _orderRepository.UpdateOrderAsync(order);

            var change = paidAmount - totalPrice;
            TempData["ChangeAmount"] = change;

            return RedirectToAction("Success");
        }
        */
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var order = await _orderRepository.GetLastUnpaidOrderAsync();
            if (order == null || !order.Items.Any())
            {
                return RedirectToAction("Index", "Catalog");
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.ChangeAmount = TempData["ChangeAmount"];
            return View();
        }
    }
}
