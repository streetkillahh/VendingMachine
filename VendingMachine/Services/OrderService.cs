using System.Threading.Tasks;
using VendingMachine.Models.Domain;
using VendingMachine.Repositories;

namespace VendingMachine.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }
    }
}
