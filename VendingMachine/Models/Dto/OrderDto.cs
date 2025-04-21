using VendingMachine.Models;

public class OrderDto
{
    public List<OrderItemDto> Items { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalInserted { get; set; }
}