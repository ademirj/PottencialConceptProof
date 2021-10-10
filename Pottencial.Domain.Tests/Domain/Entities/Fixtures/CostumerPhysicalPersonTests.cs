using Pottencial.Domain.Entities;
using System;
using Xunit;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Pottencial.Infrastructure.CrossCutting.Extension;

namespace Pottencial.Tests.Domain.Entities.Fixtures
{
    [CollectionDefinition(nameof(CostumerPhysicalPersonCollection))]
    public class CostumerPhysicalPersonCollection : ICollectionFixture<CostumerPhysicalPersonFixture>
    {

    }

    public class CostumerPhysicalPersonFixture
    {
        public CostumerPhysicalPerson CreateValidCostumerPhysicalPerson()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            return new Faker<CostumerPhysicalPerson>("pt_BR")
                .CustomInstantiator(f => new CostumerPhysicalPerson(
                        Guid.NewGuid(),
                        f.Person.Cpf().ToLong(),
                        f.Name.FirstName(gender),
                        f.Person.Email,
                        f.Person.DateOfBirth.AddYears(-25),
                        f.Random.Decimal(1000.00M, 10000.00M)));
        }

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
