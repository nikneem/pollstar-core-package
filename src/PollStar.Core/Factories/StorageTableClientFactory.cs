using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PollStar.Core.Configuration;

namespace PollStar.Core.Factories;

public class StorageTableClientFactory : IStorageTableClientFactory
{
    private readonly IOptions<AzureConfiguration> _options;

    private readonly string _accountName;

    public TableClient CreateClient(string tableName)
    {
        var tableStorageUri = new Uri($"https://{_accountName}.table.core.windows.net");

        if (string.IsNullOrWhiteSpace(_options.Value.StorageKey))
        {
            var managedIdentity = new DefaultAzureCredential();
            return new TableClient(tableStorageUri, tableName, managedIdentity);
        }

        var tableCredential = new TableSharedKeyCredential(_accountName, _options.Value.StorageKey);
        return new TableClient(tableStorageUri, tableName, tableCredential);
    }

    public StorageTableClientFactory(IOptions<AzureConfiguration> options, ILogger<StorageTableClientFactory> logger)
    {
        _options = options;
        _accountName = options.Value.StorageAccount;
    }
}