using Registrar.Api.Controllers;
using System.Linq;
using Xunit;

namespace Registrar.UnitTests
{
    public class SampleDataControllerTests
    {
        [Fact]
        public void WeatherForecasts_ReturnsData()
        {
            // Arrange
            var sampleDataController = new SampleDataController();

            // Act
            var results = sampleDataController.WeatherForecasts();

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void WeatherForecasts_ReturnsExactly5Forecasts()
        {
            // Arrange
            var sampleDataController = new SampleDataController();

            // Act
            var results = sampleDataController.WeatherForecasts();

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.Equal(5, results.Count());
        }
    }
}
