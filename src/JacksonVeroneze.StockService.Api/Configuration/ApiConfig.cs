using JacksonVeroneze.StockService.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;

namespace JacksonVeroneze.StockService.Api.Configuration
{
    public static class ApiConfig
    {
        private const string CorsPolicyName = "AllowAll";

        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true)
                .AddCorsConfiguration(CorsPolicyName)
                .HealthChecksConfiguration()
                .AddAutoMapperConfiguration()
                .AddDatabaseConfiguration(configuration)
                .AddAutoMediatRConfiguration()
                .AddDependencyInjectionConfiguration()
                .AddSwaggerConfiguration()
                .AddApplicationInsightsConfiguration(configuration)
                .AddAuthenticationConfiguration(configuration)
                .AddControllers();

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseCultureSetup()
                .UseHealthChecksSetup()
                .UseMetricServer()
                .UseHttpMetrics()
                .UseSerilogRequestLogging()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseCors(CorsPolicyName)
                .UseMiddleware<ErrorHandlingMiddleware>()
                .UseSwaggerSetup()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}
