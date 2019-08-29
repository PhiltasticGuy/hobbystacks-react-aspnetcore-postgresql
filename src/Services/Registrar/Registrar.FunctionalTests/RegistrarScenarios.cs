using Registrar.FunctionalTests.Setup;
using System.Threading.Tasks;
using Xunit;

namespace Registrar.FunctionalTests
{
    public class RegistrarScenarios : RegistrarScenarioBase
    {
        [Fact]
        public async Task GetCourses_ResponseOk()
        {
            using (var factory = new RegistrarWebApplicationFactory())
            {
                // Arrange
                var client = factory.CreateClient();

                // Act
                var response = await client.GetAsync(Get.GetCourses());
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.True(!string.IsNullOrEmpty(content));
            }
        }
    }
}
