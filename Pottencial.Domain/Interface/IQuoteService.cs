using Pottencial.Domain.Dto;

namespace Pottencial.Domain.Interface
{
    public interface IQuoteService
    {
        void QuotingInsuranceLegalPerson(CostumerLegalPersonDto costumerLegalPerson);
        void QuotingInsurancePhysicalPerson(CostumerPhysicalPersonDto costumerPhysicalPerson);
    }
}
