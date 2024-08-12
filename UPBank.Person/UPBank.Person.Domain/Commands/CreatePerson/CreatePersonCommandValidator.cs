using FluentValidation;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.CPF)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("É necessário informar o Documento")
                .Must(Validate)
                .WithMessage("CPF inválido");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("É necessário informar o nome")
                .MinimumLength(3)
                .WithMessage("Nome deve conter no mínimo 3 caracteres");

            RuleFor(x => x.BirthDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Data de nascimento inválida")
                .LessThan(DateTime.Now)
                .WithMessage("Data de nascimento não pode ser maior ou igual o dia atual");

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("É necessário informar o gênero")
                .Must(x => char.ToUpper(x) == 'M' || char.ToUpper(x) == 'F')
                .WithMessage("Gênero inválido");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("É necessário informar o email")
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("É necessário informar o telefone")
                .MinimumLength(11)
                .Must(ContainLetters)
                .WithMessage("Telefone inválido");

            RuleFor(x => x.Salary)
                .Cascade(CascadeMode.Stop)
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
            cpf = CreatePersonCommand.CpfRemoveMask(cpf);
            return CreatePersonCommand.CpfValidate(cpf);
        }
    }
}