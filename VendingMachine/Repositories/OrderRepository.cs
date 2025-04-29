using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            return order.Id;
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


        public async Task<Order> GetLastUnpaidOrderAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Catalog)
                .Where(o => !o.IsPaid)
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
