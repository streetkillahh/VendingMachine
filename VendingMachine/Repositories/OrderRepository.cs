using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VendingMachine;
using VendingMachine.Models.Domain;

namespace VendingMachine.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id; // Вернём Id созданного заказа
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task MarkOrderAsPaidAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.IsPaid = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
