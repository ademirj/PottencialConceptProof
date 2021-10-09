using Pottencial.BDD.Domain.Entities;
using System;
using Xunit;

namespace Pottencial.Tests.Domain.Entities.Fixtures
{
    [CollectionDefinition(nameof(CostumerLegalPersonCollection))]
    public class CostumerLegalPersonCollection : ICollectionFixture<CostumerLegalPersonFixture>
    {

    }

    public class CostumerLegalPersonFixture
    {
        public CostumerLegalPerson CreateValidCostumerLegalPerson()
            => new CostumerLegalPerson(
                id: Guid.NewGuid(),
                cnpj: 72313161000146,
                name: "Ademir",
                email: "michael@scott.com",
                incomeAmount: 0);

        public CostumerLegalPerson CreateInvalidCostumerLegalPerson()
            => new CostumerLegalPerson(
                id: Guid.NewGuid(),
                cnpj: 02313161000146,
                name: "Michael",
                email: "michael@scott.com",
                incomeAmount: 0);
    }
}
