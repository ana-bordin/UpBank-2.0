using MediatR;

namespace UPBank.Customer.Domain.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public string CPF { get; set; } = string.Empty;

        public DeleteCustomerCommand(string cpf)
        {
            CPF = cpf.Replace(".", "").Replace("-", "");
        }
    }
}