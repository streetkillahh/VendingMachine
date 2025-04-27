using System.Threading.Tasks;
using VendingMachine.Models.Domain;

namespace VendingMachine.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
    }
}
