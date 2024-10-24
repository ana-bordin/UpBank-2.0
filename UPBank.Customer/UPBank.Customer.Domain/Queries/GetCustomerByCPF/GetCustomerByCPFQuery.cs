using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;

namespace UPBank.Customer.Domain.Queries.GetCustomerByCPF
{
    public class GetCustomerByCPFQuery : IRequest<CustomerResponse>
    {
        public string CPF { get; set; } = string.Empty;

        public GetCustomerByCPFQuery(string cpf)
        {
            CPF = cpf.Replace(".", "").Replace("-", "");
        }
    }
}