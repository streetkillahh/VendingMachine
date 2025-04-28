using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Models.Domain;
using VendingMachine.Repositories;
using VendingMachine.Services.Models;

namespace VendingMachine.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogRepository _catalogRepository;

        public OrderService(IOrderRepository orderRepository, ICatalogRepository catalogRepository)
        {
            _orderRepository = orderRepository;
            _catalogRepository = catalogRepository;
        }

        public async Task<int> CreateOrderAsync(List<int> catalogIds)
        {
            var items = await _catalogRepository.GetCatalogItemsByIdsAsync(catalogIds);

            var orderItems = items.Select(item => new OrderItem
            {
                CatalogId = item.Id,
                Quantity = 1
            }).ToList();

            var order = new Order
            {
                Items = orderItems,
                IsPaid = false,
                CreatedAt = DateTime.UtcNow
            };

            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<PaymentResult> CompletePaymentAsync(int orderId, decimal paidAmount)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
                return new PaymentResult { Success = false, ErrorMessage = "Заказ не найден." };

            var totalPrice = order.Items.Sum(i => (i.Catalog?.Price ?? 0) * i.Quantity);

            if (paidAmount < totalPrice)
                return new PaymentResult { Success = false, ErrorMessage = "Недостаточно денег для оплаты заказа." };

            await _orderRepository.MarkOrderAsPaidAsync(orderId);

            return new PaymentResult { Success = true, Change = paidAmount - totalPrice };
        }
    }
}
