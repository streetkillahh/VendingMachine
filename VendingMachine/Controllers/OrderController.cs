using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services;
using VendingMachine.Repositories;

namespace VendingMachine.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(decimal paidAmount)
        {
            // Получение текущего неоплаченного заказа
            var order = await _orderRepository.GetLastUnpaidOrderAsync();
            if (order == null)
            {
                return RedirectToAction("Checkout"); // если заказа нет
            }

            // Проверка хватает ли внесённой суммы
            var totalPrice = order.Items.Sum(i => i.Quantity * (i.Catalog?.Price ?? 0));


            if (paidAmount < totalPrice)
            {
                TempData["PaymentError"] = "Недостаточно средств для оплаты заказа.";
                return RedirectToAction("Payment");
            }

            // Подтверждение заказа как оплаченного
            order.IsPaid = true;
            await _orderRepository.UpdateOrderAsync(order);

            // Подсчёт сдачи
            var change = paidAmount - totalPrice;

            // Передача суммы сдачи на представление Success
            TempData["ChangeAmount"] = change;

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            ViewBag.ChangeAmount = TempData["ChangeAmount"];
            return View();
        }
    }
}
