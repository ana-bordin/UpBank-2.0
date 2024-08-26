using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer;

namespace UPBank.Customer.Domain.Queries.GetCustomerByCPF
{
    public class GetCustomerByCPFQuery : IRequest<CreateCustomerCommandResponse>
    {
        public string CPF { get; set; } = string.Empty;

        public GetCustomerByCPFQuery(string cpf)
        {
            CPF = cpf.Replace(".", "").Replace("-", "");
        }
    }
}