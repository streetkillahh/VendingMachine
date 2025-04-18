namespace VendingMachine.Models.Domain
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Catalog> Catalogs { get; set; }
    }
}
