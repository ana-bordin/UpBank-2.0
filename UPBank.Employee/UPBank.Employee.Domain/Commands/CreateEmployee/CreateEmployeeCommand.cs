using MediatR;

namespace UPBank.Employee.Domain.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResponse>
    {
        public bool Manager { get; set; }
    }
}