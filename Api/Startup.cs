using System;
using System.Linq;
using Api.Common;
using Api.Extensions;
using Application;
using Bank;
using Common.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using FluentValidation.AspNetCore;
using Infrastructure;

namespace Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add a layer specific dependencies
            services.AddPersistence(Configuration);
            services.AddApplication();
            services.AddBank(Environment.IsDevelopment());
            services.AddInfrastructure();
            
            // Logging
            var loggingConfiguration = Configuration.GetSection("Logging").Get<LoggingConfiguration>();
            SerilogInitialiser.Initialise(loggingConfiguration, Environment.IsDevelopment());

            // Validation
            services.AddControllers().AddFluentValidation(config =>
                config.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic))
            );

            // Authentication to use IdentityServer
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:4000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "payment-gateway";
                });

            // Swagger documentation
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();
            
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerDocumentation();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}