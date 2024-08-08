namespace UPBank.Account.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ConclusionDate { get; set; }
        public Type Type { get; set; }
        public Account? DestinationAccount { get; set; }
        public double Value { get; set; }
    }
}
