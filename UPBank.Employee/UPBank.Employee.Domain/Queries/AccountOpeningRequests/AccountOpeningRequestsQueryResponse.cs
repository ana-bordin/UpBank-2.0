using UPBank.Employee.Domain.Queries.AccountOpeningRequests.Models;

namespace UPBank.Employee.Domain.Queries.AccountOpeningRequests
{
    public class AccountOpeningRequestsQueryResponse
    {
        public IEnumerable<Requests> Requests { get; set; }
    }
}