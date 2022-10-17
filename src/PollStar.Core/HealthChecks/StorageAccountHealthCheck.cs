using Azure.Data.Tables;
using Azure.Identity;
using HexMaster.Randomizer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using PollStar.Core.Configuration;

namespace PollStar.Core.HealthChecks;

public class StorageAccountHealthCheck : IHealthCheck
{
    private readonly IOptions<AzureConfiguration> _configOptions;
    private readonly RandomGenerator _random;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        var config = _configOptions.Value;
        try
        {
            var storageUri = new Uri($"https://{config.StorageAccount}.table.core.windows.net");
            var randomTableName = _random.GetRandomLowercaseString();
            var tableClient = string.IsNullOrWhiteSpace(config.StorageKey)
                ? new TableClient(
                    storageUri,
                    randomTableName,
                    new DefaultAzureCredential())
                : new TableClient(
                    storageUri,
                    randomTableName,
                    new TableSharedKeyCredential(config.StorageAccount, config.StorageKey));
            await tableClient.CreateIfNotExistsAsync(cancellationToken);
            await tableClient.DeleteAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }

    public StorageAccountHealthCheck(IOptions<AzureConfiguration> configOptions, RandomGenerator random)
    {
        _configOptions = configOptions;
        _random = random;
    }
}