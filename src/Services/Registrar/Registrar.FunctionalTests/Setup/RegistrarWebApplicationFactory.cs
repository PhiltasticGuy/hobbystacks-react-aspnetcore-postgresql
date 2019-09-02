using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Registrar.Api;
using Registrar.Api.Data;
using System;
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
            base.ConfigureWebHost(builder);

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

                        services.AddDbContext<RegistrarContext>();

                        // Build the service provider.
                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<RegistrarContext>();
                            var logger = scopedServices.GetRequiredService<ILogger<Program>>();

                            // Ensure the database is created.
                            db.Database.EnsureCreated();

                            try
                            {
                                // Create database on startup.
                                db.Database.Migrate();

                                // Seed the database if necessary.
                                DatabaseInitializer.Initialize(db);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred while seeding the DB.");
                            }
                        }
                    }
                )
                .ConfigureAppConfiguration((IConfigurationBuilder cb) => cb.AddEnvironmentVariables())
                .UseContentRoot(Path.GetDirectoryName(assembly.Location));
        }
    }
}
