using Pottencial.Domain.Dto;
using Pottencial.Domain.Entities;
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

        public void QuotingInsuranceLegalPerson(CostumerLegalPerson costumerLegalPerson)
        {
            if (costumerLegalPerson.IncomeAmount <= MinValueInsuranceLegalPerson)
                throw new DomainException(string.Format(DomainMessages.IncomeAmountNotEnoughToQuote, MinValueInsurancePhysicalPerson));

            if (_blackListService.ListLegalPerson().Any(p => p == costumerLegalPerson.Cnpj))
                throw new DomainException(DomainMessages.CostumerInBlackList);
        }

        public void QuotingInsurancePhysicalPerson(CostumerPhysicalPerson costumerPhysicalPerson)
        {
            if (costumerPhysicalPerson.IncomeAmount <= MinValueInsuranceLegalPerson)
                throw new DomainException(string.Format(DomainMessages.IncomeAmountNotEnoughToQuote, MinValueInsurancePhysicalPerson));

            if (costumerPhysicalPerson.CalculateAge() > MaxAgeAllowed)
                throw new DomainException(string.Format(DomainMessages.MaxAgeAllowed, MaxAgeAllowed));

            if (_blackListService.ListPhysicalPerson().Any(p => p == costumerPhysicalPerson.Cpf))
                throw new DomainException(DomainMessages.CostumerInBlackList);
        }

        public decimal GetMinIncomeAmountToQuotingInsuranceLegalPerson()
        {
            return MinValueInsuranceLegalPerson;
        }
    }
}
