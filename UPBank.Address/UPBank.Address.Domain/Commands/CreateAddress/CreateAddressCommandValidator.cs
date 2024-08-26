using FluentValidation;

namespace UPBank.Address.Domain.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(command => command.ZipCode)
                .NotEmpty()
                .WithMessage("O CEP é obrigatório");

            RuleFor(command => command.Number)
                .NotEmpty()
                .Matches(@"^[1-9]\d*$")
                .WithMessage("O número é obrigatório");
        }
    }
}