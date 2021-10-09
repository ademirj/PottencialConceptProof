using Pottencial.Domain.Interface;
using System.Collections.Generic;

namespace Pottencial.Domain.Services
{
    public class BlackListService : IBlackListService
    {
        public List<long> ListLegalPerson()
        {
            return new List<long>()
            {
                41867830000106,
                80394886000134
            };
        }

        public List<long> ListPhysicalPerson()
        {
            return new List<long>()
            {
                52380578060,
                95508855009
            };
        }
    }
}
