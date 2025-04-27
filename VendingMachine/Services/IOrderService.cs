using System.Threading.Tasks;
using VendingMachine.Models.Domain;
using VendingMachine.Services.Models;

namespace VendingMachine.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(Order order);
        Task<PaymentResult> CompletePaymentAsync(int orderId, decimal paidAmount);
    }
}
