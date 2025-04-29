namespace VendingMachine.Models.Domain
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
