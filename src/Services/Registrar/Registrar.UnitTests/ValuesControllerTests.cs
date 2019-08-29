using Registrar.Api.Controllers;
using Xunit;

namespace Registrar.UnitTests
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Get_ReturnsExpectedValues()
        {
            // Arrange
            var valuesController = new ValuesController();

            // Act
            var actionResult = valuesController.Get();

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Assert.Contains("value1", actionResult.Value);
            Assert.Contains("value2", actionResult.Value);
        }
    }
}
