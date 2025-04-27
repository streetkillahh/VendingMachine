namespace VendingMachine.Models.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
        public int Quantity { get; set; }

        // Вычисляемое свойство — безопасно берёт цену
        public decimal Price => (Catalog?.Price ?? 0) * Quantity;
    }
}
