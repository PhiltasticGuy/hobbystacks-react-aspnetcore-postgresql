using Microsoft.Extensions.Configuration;
using Registrar.Api;

namespace Registrar.FunctionalTests.Setup
{
    public class RegistrarTestsStartup : Startup
    {
        public RegistrarTestsStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
