namespace VendingMachine.Services.Models
{
    public class PaymentResult
    {
        public bool Success { get; set; }
        public decimal Change { get; set; }
        public string ErrorMessage { get; set; }
    }
}
