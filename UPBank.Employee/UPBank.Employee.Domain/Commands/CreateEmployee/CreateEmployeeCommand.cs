using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Employee.Domain.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : CreatePersonCommand, IRequest<CreateEmployeeCommandResponse>
    {
        public bool Manager { get; set; }
    }
}