using System.ComponentModel.DataAnnotations;

namespace UPBank.Person.Domain.Entities
{
    public class Person
    {
        [Key]
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //[NotMapped]
        //public CompleteAddress Address { get; set; }
        public Guid AddressId { get; set; }

        public string Validate(Person person)
        {
            if (string.IsNullOrEmpty(person.CPF))
                return "É necessário cadastrar CPF";

            if (CpfValidate(person.CPF))
                return "CPF inválido";

            if (string.IsNullOrEmpty(Name) || person.Name.Length < 3)
                return "Nome inválido";

            if (person.BirthDate > DateTime.Now)
                return "Data de nascimento inválida";

            if (string.IsNullOrEmpty(Email) || !person.Email.Contains("@") || !person.Email.Contains("."))
                return "Email inválido";

            if (string.IsNullOrEmpty(Phone) || person.Phone.Length < 11)
                return "Telefone inválido";

            if (person.Salary < 0)
                return "Salário inválido";

            return string.Empty;
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
