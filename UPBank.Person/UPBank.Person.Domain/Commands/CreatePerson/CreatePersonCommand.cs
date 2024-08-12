using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest<CreatePersonCommandResponse>
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CreateAddressCommand Address { get; set; }

        public static string CpfRemoveMask(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }
        public string CpfAddMask(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static bool CpfValidate(string cpf)
        {
            string tempCpf = cpf.Substring(0, 9);

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * (10 - i);
            }

            int checkFirstDigit = 11 - (sum % 11);
            if (checkFirstDigit > 9)
                checkFirstDigit = 0;

            tempCpf += checkFirstDigit;

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * (11 - i);
            }

            int checkSecondDigit = 11 - (sum % 11);

            if (checkSecondDigit > 9)
                checkSecondDigit = 0;

            return cpf.EndsWith(checkFirstDigit.ToString() + checkSecondDigit.ToString()) || cpf.Length != 11 ? true : false;
        }
    }
}