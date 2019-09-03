using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registrar.Api.Data;

namespace Registrar.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add PostgresSQL support.
            var connectionString = Configuration["DATABASE_URL"];
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<RegistrarContext>(
                    options => options.UseNpgsql(connectionString)
                )
                .AddTransient<ICourseRepository, CourseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //TODO: URLs should be in settings.
            app.UseCors(
                builder =>
                {
                    builder
                        .WithOrigins("https://localhost:9001", "http://localhost:8001")
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                }
            );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
