using UPBank.Utils.Enums;

namespace UPBank.Account.Domain.Entities
{
    public class Account
    {
        public Agency.Domain.Entities.Agency Agency { get; set; }  
        public string AccountNumber { get; set; }
        public List<Customer.Domain.Entities.Customer> Customer { get; set; }
        public bool Restriction { get; set; }
        public CreditCard CreditCard { get; set; }
        public double Overdraft { get; set; }
        public Profile Profile { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Balance { get; set; }
        public double Statement { get; set; }
        public bool Active { get; set; }
    }
}
