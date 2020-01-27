using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "PaymentGatewayOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Payment Gateway API",
                        Version = "v1.0"
                    });
                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      <br>Example: 'Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkpwY1dFeXh4R25fcnYyTmEtcV9HOVEiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE1ODAxNjA4MTgsImV4cCI6MTU4MDE2NDQxOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo0MDAwIiwiYXVkIjoicGF5bWVudC1nYXRld2F5IiwiY2xpZW50X2lkIjoibWVyY2hhbnQiLCJzY29wZSI6WyJwYXltZW50LWdhdGV3YXkiXX0.Goatwh9KTl85d3P-Clw3RKGszqFohdIQyX0q3nk0aMmlp1tgPhupi6jae56rRZhZMaJRxRHOvLneEVUtIKjl7zq8qPY2dzRgrVxxjzqkGJmy-wK1moUZldexFvRrob8eQ8vqHm_gCmqZNu5LaysIOccrTj_VL1un5sKuo6WNO_S7wxxLrrqmrv9mHMTB9DjnHg1jZJi71WbMw66137u02DpyLfKq_Sk9nX2CADWWrJzql2yejrQ0_FxKaLp3UvVi9NftQz6_nICnfEC9G506kwvNI3Ff0_iWfhLKm48OGGMqLVNS8arXtCHF7rC-QPc2O8m8c_YxJCfGdHOuEVeC0w'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/PaymentGatewayOpenAPISpecification/swagger.json",
                    "Payment Gateway API");
                setupAction.RoutePrefix = "";
            });
            
            return app;
        }
    }
}