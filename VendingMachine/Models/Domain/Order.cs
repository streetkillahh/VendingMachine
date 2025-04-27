using System;
using System.Collections.Generic;

namespace VendingMachine.Models.Domain;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public bool IsPaid { get; set; } = false;
}
