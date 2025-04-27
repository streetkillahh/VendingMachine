using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public bool IsPaid { get; set; } = false;

        // TotalPrice считается на лету
        public decimal TotalPrice => Items?.Sum(i => i.Quantity * i.Catalog.Price) ?? 0m;
    }
}
