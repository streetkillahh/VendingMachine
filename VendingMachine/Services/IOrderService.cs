using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Models.Domain;
using VendingMachine.Services.Models;

namespace VendingMachine.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(List<int> catalogIds);
        Task<PaymentResult> CompletePaymentAsync(int orderId, decimal paidAmount);
    }
}
