using Pottencial.BDD.Domain.Entities;
using System;
using Xunit;

namespace Pottencial.Tests.Domain.Entities.Fixtures
{
    [CollectionDefinition(nameof(CostumerPhysicalPersonCollection))]
    public class CostumerPhysicalPersonCollection : ICollectionFixture<CostumerPhysicalPersonFixture>
    {

    }

    public class CostumerPhysicalPersonFixture
    {
        public CostumerPhysicalPerson CreateValidCostumerPhysicalPerson()
            => new CostumerPhysicalPerson(
                id: Guid.NewGuid(),
                cpf: 42689694034,
                name: "Ademir",
                email: "michael@scott.com",
                birthDate: new DateTime(1988, 10, 22),
                incomeAmount: 0);

        public CostumerPhysicalPerson CreateInvalidCostumerPhysicalPerson()
            => new CostumerPhysicalPerson(
                id: Guid.NewGuid(),
                cpf: 123,
                name: "Ademir",
                email: "michael@scott.com",
                birthDate: new DateTime(1988, 10, 22),
                incomeAmount: 0);
    }
}
