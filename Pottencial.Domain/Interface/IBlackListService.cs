using System.Collections.Generic;

namespace Pottencial.Domain.Interface
{
    public interface IBlackListService
    {
        List<long> ListLegalPerson();
        List<long> ListPhysicalPerson();
    }
}
