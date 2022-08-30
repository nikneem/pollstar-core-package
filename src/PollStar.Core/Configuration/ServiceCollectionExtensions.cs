using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PollStar.Core.HealthChecks;

namespace PollStar.Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPollStarCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck<StorageAccountHealthCheck>("StorageAccountHealthCheck");

        services.Configure<AzureConfiguration>(configuration.GetSection(AzureConfiguration.SectionName));
        return services;
    }
}