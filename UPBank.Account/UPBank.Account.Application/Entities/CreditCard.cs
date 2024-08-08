namespace UPBank.Account.Domain.Entities
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public double Limit { get; set; }
        public string SecurityCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}
