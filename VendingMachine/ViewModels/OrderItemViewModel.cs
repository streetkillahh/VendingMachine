namespace VendingMachine.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string ImageUrl
        {
            get
            {
                string fileName =
                    Name.ToLower().Contains("cola") ? "cola.png" :
                    Name.ToLower().Contains("fanta") ? "fanta.png" :
                    Name.ToLower().Contains("sprite") ? "sprite.png" :
                    Name.ToLower().Contains("pepper") ? "drpepper.png" :
                    Name.ToLower().Contains("chernogolovka") ? "chernogolovka.png" :
                    Name.ToLower().Contains("irn") ? "irnbru.png" :
                    Name.ToLower().Contains("mountain") ? "mountaindew.png" :
                    Name.ToLower().Contains("pepsi") ? "pepsi.png" :
                    "placeholder.png";

                return $"/images/{fileName}";
            }
        }
    }
}
