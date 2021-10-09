using FluentValidation;
using Pottencial.Domain.Entities;
using Pottencial.Infrastructure.CrossCutting.Exceptions;
using System;
using System.Linq;

namespace Pottencial.BDD.Domain.Entities
{
    public class CostumerPhysicalPerson : Costumer
    {
        public long Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CostumerPhysicalPerson(long cpf, string name, string email, DateTime birthDate, decimal incomeAmount)
            : base(name, email, incomeAmount)
        {
            this.Cpf = cpf;
            this.BirthDate = birthDate;
        }

        protected override void Validate()
        {
            var validation = new CostumerPhysicalPersonValidation().Validate(this);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
                throw new DomainException($"{ string.Join(";", errors) }");
            }
        }

        public int CalculateAge()
        {
            var age = DateTime.Now.Year - BirthDate.Year;

            return DateTime.Now.DayOfYear < BirthDate.DayOfYear ? age-- : age;
        }


        public class CostumerPhysicalPersonFactory
        {
            public static CostumerPhysicalPerson Create(long cpf, string name, string email, DateTime birthDate, decimal incomeAmount)
            {
                return new CostumerPhysicalPerson(cpf, name, email, birthDate, incomeAmount);
            }
        }
    }

    public class CostumerPhysicalPersonValidation : AbstractValidator<CostumerPhysicalPerson>
    {
        public CostumerPhysicalPersonValidation()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty()
                .Must(IsCpf)
                .WithMessage("Invalid Cpf");
        }

        public static bool IsCpf(long cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpfString = cpf.ToString().Trim().Replace(".", "").Replace("-", "");
            if (cpfString.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpfString)
                    return false;

            string tempCpf = cpfString.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpfString.EndsWith(digito);
        }
    }
}
