using AutoMapper;
using MediatR;
using UPBank.Employee.Domain.Contracts;
using UPBank.Employee.Domain.Queries.AccountOpeningRequests.Models;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.Integration.Person.Contracts;

namespace UPBank.Employee.Domain.Queries.AccountOpeningRequests
{
    public class AccountOpeningRequestsQueryHandler : IRequestHandler<AccountOpeningRequestsQuery, AccountOpeningRequestsQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IPersonService _personService;

        public async Task<AccountOpeningRequestsQueryResponse> Handle(AccountOpeningRequestsQuery request, CancellationToken cancellationToken)
        {
            var allRequests = await _employeeRepository.AccountOpeningRequests();
            var requests = new Requests();
            foreach (var requestAccount in allRequests)
            {
                var person = _personService.GetPersonByCPFAsync(requestAccount.CPF);

                var test = _mapper.Map<AccountOpeningRequest>(person);
                test.Request = requestAccount.Request;

                if (requests.AccountOpeningRequest.Any(x => x.Request == requestAccount.Request))
                    requests.AccountOpeningRequest.Where(x => x.Request == requestAccount.Request).Append(test);
                else
                    requests.AccountOpeningRequest = new List<AccountOpeningRequest>().Append(test);
            }

            return new AccountOpeningRequestsQueryResponse() { Requests = new List<Requests>().Append(requests) };
        }

    }
}