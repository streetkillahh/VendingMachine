using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Models.Domain;

namespace VendingMachine.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Отображение страницы корзины
        public IActionResult Checkout()
        {
            return View();
        }
       
        // Отображение страницы оплаты
        [HttpGet]
        public IActionResult Payment(decimal total)
        {
            ViewBag.Total = total;
            return View();
        }

        // Страница успеха после оплаты
        public IActionResult Success()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveOrder([FromBody] OrderDto orderDto)
        {
            Console.WriteLine($"Получен заказ: {System.Text.Json.JsonSerializer.Serialize(orderDto)}");

            if (orderDto.TotalInserted < orderDto.TotalPrice)
            {
                return BadRequest("Недостаточно средств.");
            }

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                TotalPrice = orderDto.TotalPrice,
                Items = new List<OrderItem>()
            };

            foreach (var item in orderDto.Items)
            {
                var product = _context.Catalogs.FirstOrDefault(p => p.Id == item.Id);
                if (product == null)
                {
                    return BadRequest(new { error = $"Товар с ID {item.Id} не найден." });

                }

                if (product.Quantity < 1)
                {
                    return BadRequest(new { error = $"Товар с ID {item.Id} не найден." });

                }

                if (product == null)
                {
                    return BadRequest(new { error = $"Товар с ID {item.Id} не найден." });

                }

                order.Items.Add(new OrderItem
                {
                    CatalogId = product.Id,
                    Quantity = 1,
                    UnitPrice = product.Price
                });

                product.Quantity -= 1;
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            decimal change = orderDto.TotalInserted - orderDto.TotalPrice;
            return Ok(new { success = true, change });
        }


    }
}
