using MediatR;

namespace UPBank.Employee.Domain.Commands
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResponse>
    {
        public bool Manager { get; set; }
    }
}