using Pottencial.Infrastructure.CrossCutting.Messages;
using Pottencial.Tests.Domain.Entities.Fixtures;
using System.Linq;
using Xunit;

namespace Pottencial.Tests.Domain.Entities
{

    [Collection(nameof(CostumerPhysicalPersonCollection))]
    public class CostumerPhysicalPersonTests
    {
        private readonly CostumerPhysicalPersonFixture _costumerPhysicalPersonFixture;

        public CostumerPhysicalPersonTests(CostumerPhysicalPersonFixture costumerPhysicalPersonFixture)
        {
            _costumerPhysicalPersonFixture = costumerPhysicalPersonFixture;
        }

        [Fact(DisplayName = "CostumerPhysicalPerson is valid")]
        [Trait("CostumerPhysicalPersonTests", "CostumerPhysicalPerson Tests")]
        public void CostumerPhysicalPerson_ValidateCpf_ShouldBeValid()
        {
            // Arrage
            var costumer = _costumerPhysicalPersonFixture.CreateValidCostumerPhysicalPerson();

            // Act
            var validationResult = costumer.IsValid();

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact(DisplayName = "CostumerPhysicalPerson must have invalid cpf")]
        [Trait("CostumerPhysicalPersonTests", "CostumerPhysicalPerson Tests")]
        public void CostumerPhysicalPerson_ValidateCpf_ShouldBeInvalid()
        {
            // Arrage
            var costumer = _costumerPhysicalPersonFixture.CreateInvalidCostumerPhysicalPerson();

            // Act
            var validationResult = costumer.IsValid();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InvalidCpf, validationResult.Errors.FirstOrDefault().ErrorMessage);
        }
    }
}
