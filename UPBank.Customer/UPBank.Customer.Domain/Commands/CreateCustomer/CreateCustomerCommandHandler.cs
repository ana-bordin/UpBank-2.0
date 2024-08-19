using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Contracts;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
    {
        public readonly ICustomerRepository _customerRepository;
        private readonly IPersonService _personService;
        private IMapper _mapper;
        public Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}