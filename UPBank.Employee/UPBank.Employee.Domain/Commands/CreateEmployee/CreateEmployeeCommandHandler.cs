using AutoMapper;
using MediatR;
using UPBank.Employee.Domain.Contracts;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.Integration.Person.Contracts;

namespace UPBank.Employee.Domain.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IMapper _mapper;
        public readonly IDomainNotificationService _domainNotificationService;
        public readonly IPersonService _personService;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper, IDomainNotificationService domainNotificationService, IPersonService personService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
            _personService = personService;
        }

        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var person = _personService.CreatePersonAsync(request);
            if (person == null)
                return null;

            var employee = await _employeeRepository.AddAsync(_mapper.Map<CreateEmployeeCommand, Entities.Employee>(request));

            if (employee == null)
                return null;

            var result = _mapper.Map<Entities.Employee, CreateEmployeeCommandResponse>(employee);
            _mapper.Map(person, result);

            return result;
        }
    }
}