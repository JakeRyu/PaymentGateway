using System;
using Api.Common;
using Application;
using Application.Features.Payments.Commands.CreatePayment;
using Bank;
using Common.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using FluentValidation.AspNetCore;

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
            services.AddInfrastructure(Environment.IsDevelopment());
            
            // Logging
            var loggingConfiguration = Configuration.GetSection("Logging").Get<LoggingConfiguration>();
            SerilogInitialiser.Initialise(loggingConfiguration, Environment.IsDevelopment());

            // Validation
            services.AddControllers().AddFluentValidation(config =>
                config.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            );

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "PaymentGatewayOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Payment Gateway API",
                        Version = "1"
                    });
            });

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
            
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/PaymentGatewayOpenAPISpecification/swagger.json",
                    "Payment Gateway API");
                setupAction.RoutePrefix = "";
            });
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}