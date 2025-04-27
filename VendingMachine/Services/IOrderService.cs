using System.Threading.Tasks;
using VendingMachine.Models.Domain;

namespace VendingMachine.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
    }
}
