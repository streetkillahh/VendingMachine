using System;
using System.Collections.Generic;

namespace VendingMachine.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        // Вычисляемое свойство, НЕ сохраняется в БД
        public decimal TotalPrice
        {
            get
            {
                return Items?.Sum(item => (item.Catalog?.Price ?? 0) * item.Quantity) ?? 0;
            }
        }
    }
}
