using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registrar.Api;
using System.IO;
using System.Reflection;

namespace Registrar.FunctionalTests.Setup
{
    public class RegistrarWebApplicationFactory : WebApplicationFactory<RegistrarTestsStartup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<RegistrarTestsStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(Startup));

            // Add the Registrar.Api assembly as an Application Part for MVC.
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-2.2
            // https://github.com/aspnet/AspNetCore.Docs/issues/7825
            // https://stackoverflow.com/questions/52142893/error-on-inherit-startup-in-asp-net-core-2-1-functional-tests
            builder
                .ConfigureServices(
                    (IServiceCollection services) =>
                    {
                        services
                            .AddMvc()
                            .AddApplicationPart(assembly);
                    }
                )
                .ConfigureAppConfiguration((IConfigurationBuilder cb) => cb.AddEnvironmentVariables())
                .UseContentRoot(Path.GetDirectoryName(assembly.Location));
            
            base.ConfigureWebHost(builder);
        }
    }
}
