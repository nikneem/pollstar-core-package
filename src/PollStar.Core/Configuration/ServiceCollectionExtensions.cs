using HexMaster.Randomizer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PollStar.Core.Factories;
using PollStar.Core.HealthChecks;

namespace PollStar.Core.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPollStarCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck<StorageAccountHealthCheck>("StorageAccountHealthCheck");

        services.AddTransient<IServiceBusClientFactory, ServiceBusClientFactory>();
        services.AddTransient<IStorageQueueClientFactory, StorageQueueClientFactory>();
        services.AddTransient<IStorageTableClientFactory, StorageTableClientFactory>();

        services.AddRandomizer();
        services.Configure<AzureConfiguration>(configuration.GetSection(AzureConfiguration.SectionName));
        return services;
    }
}