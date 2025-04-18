namespace VendingMachine.Models.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int CatalogId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Catalog Catalog { get; set; }
    }
}
