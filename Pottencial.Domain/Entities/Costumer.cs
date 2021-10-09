using FluentValidation;
using Pottencial.Infrastructure.CrossCutting.Exceptions;
using System;

using System.Linq;
namespace Pottencial.Domain.Entities
{
    public abstract class Costumer
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public decimal IncomeAmount { get; private set; }

        public Costumer(string name, string email, decimal incomeAmount)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
            this.IncomeAmount = incomeAmount;

            Validate();
        }

        protected virtual void Validate()
        {
            var validation = new CostumerValidation().Validate(this);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
                throw new DomainException($"{ string.Join(";", errors) }");
            }
        }
    }

    public class CostumerValidation : AbstractValidator<Costumer>
    {
        public CostumerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please, check if you informed a name")
                .Length(2, 150).WithMessage("The name must have between 2 and 150 characters");

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.IncomeAmount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
