using Pottencial.BDD.Domain.Entities;
using Pottencial.Domain.Dto;
using Pottencial.Domain.Interface;
using Pottencial.Infrastructure.CrossCutting.Exceptions;
using Pottencial.Infrastructure.CrossCutting.Messages;
using System;
using System.Linq;

namespace Pottencial.Domain.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IBlackListService _blackListService;
        
        private const decimal MinValueInsuranceLegalPerson = 3000;
        private const decimal MinValueInsurancePhysicalPerson = 45000;
        private const int MaxAgeAllowed = 60;

        public QuoteService(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }

        public void QuotingInsuranceLegalPerson(CostumerLegalPersonDto costumerLegalPerson)
        {
            var costumer = CostumerLegalPerson.CostumerLegalPersonFactory.Create(
                id: Guid.NewGuid(),
                cnpj: costumerLegalPerson.Cnpj, 
                name: costumerLegalPerson.Name, 
                email: costumerLegalPerson.Email, 
                incomeAmount: costumerLegalPerson.IncomeAmount);

            if (costumer.IncomeAmount <= MinValueInsuranceLegalPerson)
                throw new DomainException(string.Format(DomainMessages.IncomeAmountNotEnoughToQuote, MinValueInsurancePhysicalPerson));

            if (_blackListService.ListLegalPerson().Any(p => p == costumer.Cnpj))
                throw new DomainException(DomainMessages.CostumerInBlackList);
        }

        public void QuotingInsurancePhysicalPerson(CostumerPhysicalPersonDto costumerPhysicalPerson)
        {
            var costumer = CostumerPhysicalPerson.CostumerPhysicalPersonFactory.Create(
                id: Guid.NewGuid(),
                cpf: costumerPhysicalPerson.Cpf,
                name: costumerPhysicalPerson.Name, 
                email: costumerPhysicalPerson.Email, 
                birthDate: costumerPhysicalPerson.BirthDate, 
                incomeAmount: costumerPhysicalPerson.IncomeAmount);

            if (costumer.IncomeAmount <= MinValueInsuranceLegalPerson)
                throw new DomainException(string.Format(DomainMessages.IncomeAmountNotEnoughToQuote, MinValueInsurancePhysicalPerson));

            if (costumer.CalculateAge() >= MaxAgeAllowed)
                throw new DomainException(string.Format(DomainMessages.MaxAgeAllowed, MaxAgeAllowed));

            if (_blackListService.ListPhysicalPerson().Any(p => p == costumer.Cpf))
                throw new DomainException(DomainMessages.CostumerInBlackList);
        }
    }
}
