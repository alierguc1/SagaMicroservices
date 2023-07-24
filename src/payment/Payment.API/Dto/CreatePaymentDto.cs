namespace Payment.API.Dto
{
    public class CreatePaymentDto
    {
        public string BuyerId { get; set; }
        public int balance { get; set; }
    }
}
