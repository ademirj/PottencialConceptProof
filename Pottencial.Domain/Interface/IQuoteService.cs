using Pottencial.Domain.Entities;

namespace Pottencial.Domain.Interface
{
    public interface IQuoteService
    {
        decimal GetMinIncomeAmountToQuotingInsuranceLegalPerson();
        void QuotingInsuranceLegalPerson(CostumerLegalPerson costumerLegalPerson);
        void QuotingInsurancePhysicalPerson(CostumerPhysicalPerson costumerPhysicalPerson);
    }
}
