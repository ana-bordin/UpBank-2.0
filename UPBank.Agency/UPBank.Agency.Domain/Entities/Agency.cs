namespace UPBank.Agency.Domain.Entities
{
    public class Agency
    {
        public string CNPJ { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public List<Employee.Domain.Entities.Employee> Employees { get; set; }
        public bool Active { get; set; }
        public bool Restricted { get; set; }
    }
}