using Pottencial.Infrastructure.CrossCutting.Messages;
using Pottencial.Tests.Domain.Entities.Fixtures;
using System.Linq;
using Xunit;

namespace Pottencial.Tests.Domain.Entities
{
    [Collection(nameof(CostumerLegalPersonCollection))]
    public class CostumerLegalPersonTests
    {
        private readonly CostumerLegalPersonFixture _costumerLegalPersonFixture;

        public CostumerLegalPersonTests(CostumerLegalPersonFixture costumerLegalPersonFixture)
        {
            _costumerLegalPersonFixture = costumerLegalPersonFixture;
        }

        [Fact(DisplayName = "CostumerLegalPerson is valid")]
        [Trait("CostumerLegalPersonTests", "CostumerLegalPersonTests Tests")]
        public void CostumerLegalPerson_NewClient_ShouldBeValid()
        {
            // Arrage
            var costumer = _costumerLegalPersonFixture.CreateValidCostumerLegalPerson();

            // Act
            var validationResult = costumer.IsValid();

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact(DisplayName = "CostumerLegalPerson must have invalid cnpj")]
        [Trait("CostumerLegalPersonTests", "CostumerLegalPersonTests Tests")]
        public void CostumerLegalPerson_ValidateCnpj_ShouldBeInvalid()
        {
            // Arrage
            var costumer = _costumerLegalPersonFixture.CreateInvalidCostumerLegalPerson();

            // Act
            var validationResult = costumer.IsValid();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InvalidCnpj, validationResult.Errors.FirstOrDefault().ErrorMessage);
        }
    }
}
