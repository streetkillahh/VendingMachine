using System.Threading.Tasks;
using VendingMachine.Models.Domain;
using VendingMachine.Repositories;
using VendingMachine.Services.Models;

namespace VendingMachine.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<PaymentResult> CompletePaymentAsync(int orderId, decimal paidAmount)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
                return new PaymentResult { Success = false, ErrorMessage = "Заказ не найден." };

            var totalPrice = order.Items.Sum(i => i.Price * i.Quantity);

            if (paidAmount < totalPrice)
                return new PaymentResult { Success = false, ErrorMessage = "Недостаточно денег для оплаты заказа." };

            // Здесь можно отметить заказ как оплаченный или что-то ещё
            await _orderRepository.MarkOrderAsPaidAsync(orderId);

            return new PaymentResult { Success = true, Change = paidAmount - totalPrice };
        }
    }
}
