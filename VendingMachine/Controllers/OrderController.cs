using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Repositories;
using VendingMachine.Services;
using VendingMachine.Services.Models;
using VendingMachine.ViewModels;

namespace VendingMachine.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;

        public OrderController(ICatalogRepository catalogRepository, IOrderService orderService, IOrderRepository orderRepository)
        {
            _catalogRepository = catalogRepository;
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        // Страница выбора напитков
        public async Task<IActionResult> Index()
        {
            var catalogItems = await _catalogRepository.GetAllAsync();
            return View(catalogItems);
        }

        // Создание заказа
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] List<int> selectedCatalogIds)
        {
            if (selectedCatalogIds == null || !selectedCatalogIds.Any())
            {
                return BadRequest("Ничего не выбрано.");
            }

            await _orderService.CreateOrderAsync(selectedCatalogIds);

            return Ok(new { redirectUrl = Url.Action("Checkout") });
        }

        // Страница корзины (оформление заказа)
        public async Task<IActionResult> Checkout()
        {
            var order = await _orderRepository.GetLastUnpaidOrderAsync();
            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = order.Items.Select(i => new OrderItemViewModel
            {
                Name = i.Catalog?.Name,
                Price = i.Catalog?.Price ?? 0,
                Quantity = i.Quantity,
                ImageUrl = GetImageForDrink(i.Catalog?.Name)
            }).ToList();

            return View(viewModel);
        }

        private string GetImageForDrink(string drinkName)
        {
            if (string.IsNullOrEmpty(drinkName)) return "placeholder.png";

            drinkName = drinkName.ToLower();
            return drinkName.Contains("cola") ? "cola.png" :
                   drinkName.Contains("fanta") ? "fanta.png" :
                   drinkName.Contains("sprite") ? "sprite.png" :
                   drinkName.Contains("pepper") ? "drpepper.png" :
                   drinkName.Contains("chernogolovka") ? "chernogolovka.png" :
                   drinkName.Contains("irn") ? "irnbru.png" :
                   drinkName.Contains("mountain") ? "mountaindew.png" :
                   drinkName.Contains("pepsi") ? "pepsi.png" :
                   "placeholder.png";
        }


        // Страница оплаты
        public async Task<IActionResult> Payment()
        {
            var order = await _orderRepository.GetLastUnpaidOrderAsync();
            if (order == null)
            {
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // Подтверждение оплаты
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(decimal paidAmount)
        {
            var order = await _orderRepository.GetLastUnpaidOrderAsync();
            if (order == null)
            {
                return RedirectToAction("Index");
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

        // Страница успешной оплаты
        public IActionResult Success()
        {
            ViewBag.ChangeAmount = TempData["ChangeAmount"];
            return View();
        }
    }
}
