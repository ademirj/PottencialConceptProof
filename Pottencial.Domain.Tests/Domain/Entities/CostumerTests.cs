using Pottencial.Domain.Entities;
using Xunit;

namespace Pottencial.Tests.Domain.Entities
{
    public class CostumerTests
    {
        [Fact(DisplayName = "Check if the Costumer class is abstract")]
        [Trait("Costumer", "Valid class")]
        public void Costumer_CheckIsAbstractClass_Yes()
        {
            // Arrange, Act, Assert
            Assert.True(typeof(Costumer).IsAbstract);
        }
    }
}
