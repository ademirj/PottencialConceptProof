using FluentValidation;
using Pottencial.Domain.Entities;
using Pottencial.Infrastructure.CrossCutting.Exceptions;
using System.Linq;

namespace Pottencial.BDD.Domain.Entities
{
    public class CostumerLegalPerson : Costumer
    {
        public long Cnpj { get; private set; }

        public CostumerLegalPerson(long cnpj, string name, string email, decimal incomeAmount)
            : base(name, email, incomeAmount)
        {
            this.Cnpj = cnpj;
        }

        protected override void Validate()
        {
            var validation = new CostumerLegalPersonValidation().Validate(this);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
                throw new DomainException($"{ string.Join(";", errors) }");
            }
        }

        public class CostumerLegalPersonFactory
        {
            public static CostumerLegalPerson Create(long cnpj, string name, string email, decimal incomeAmount)
            {
                return new CostumerLegalPerson(cnpj, name, email, incomeAmount);
            }
        }
    }

    public class CostumerLegalPersonValidation : AbstractValidator<CostumerLegalPerson>
    {
        public CostumerLegalPersonValidation()
        {
            RuleFor(c => c.Cnpj)
                .NotEmpty()
                .Must(IsCnpj)
                .WithMessage("Invalid Cnpj");
        }

        public static bool IsCnpj(long cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cnpjString = cnpj.ToString().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpjString.Length != 14)
                return false;

            string tempCnpj = cnpjString.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpjString.EndsWith(digito);
        }
    }
}