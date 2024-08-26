using MediatR;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Domain.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDomainNotificationService _domainNotificationService;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IDomainNotificationService domainNotificationService)
        {
            _customerRepository = customerRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.DeleteAsync(request.CPF);
        }
    }
}