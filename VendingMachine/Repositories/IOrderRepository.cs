using System.Threading.Tasks;
using VendingMachine.Models.Domain;

namespace VendingMachine.Repositories
{
    public interface IOrderRepository
    {
        Task<int> AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task MarkOrderAsPaidAsync(int orderId);
        Task<Order> GetLastUnpaidOrderAsync();
        Task UpdateOrderAsync(Order order);
    }
}
