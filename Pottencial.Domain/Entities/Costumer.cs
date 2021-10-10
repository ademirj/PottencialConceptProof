using FluentValidation;
using FluentValidation.Results;
using Pottencial.Infrastructure.CrossCutting.Exceptions;
using Pottencial.Infrastructure.CrossCutting.Messages;
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

        public Costumer(Guid id, string name, string email, decimal incomeAmount)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.IncomeAmount = incomeAmount;
        }

        public virtual ValidationResult IsValid()
        {
            return new CostumerValidation().Validate(this);
        }

        public void SetIncomeAmount(decimal incomeAmount)
        {
            if (incomeAmount <= 0)
                throw new DomainException(DomainMessages.IncomeAmountCannotBeEmpty);

            this.IncomeAmount = incomeAmount;
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
