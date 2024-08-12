using FluentValidation;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommandValidation : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidation()
        {
            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("É necessário informar o CPF")
                .Length(11)
                .WithMessage("CPF deve conter 11 caracteres")
                .Must(Validate)
                .WithMessage("CPF inválido");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("É necessário informar o nome")
                .MinimumLength(3)
                .WithMessage("Nome deve conter no mínimo 3 caracteres");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Data de nascimento inválida")
                .LessThan(DateTime.Now)
                .WithMessage("Data de nascimento não pode ser maior ou igual o dia atual");
            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("É necessário informar o gênero");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("É necessário informar o email")
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("É necessário informar o telefone")
                .MinimumLength(11)
                .Must(ContainLetters)
                .WithMessage("Telefone inválido");

            RuleFor(x => x.Salary)
                .NotEmpty()
                .WithMessage("É necessário informar o salário")
                .GreaterThan(0)
                .WithMessage("Salário inválido");
        }

        private bool ContainLetters(string phone)
        {
            return !phone.Any(char.IsLetter);
        }
        private bool Validate(string cpf)
        {
            return CreatePersonCommand.CpfValidate(cpf);
        }
    }
}