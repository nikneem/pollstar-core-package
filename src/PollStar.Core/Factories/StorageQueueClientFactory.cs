using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PollStar.Core.Configuration;

namespace PollStar.Core.Factories;

public class StorageQueueClientFactory : IStorageQueueClientFactory
{

    private readonly string _accountName;
    
    public QueueClient CreateClient(string queueName)
    {
        var identity = new DefaultAzureCredential();
        var queueStorageUri = new Uri($"https://{_accountName}.queue.core.windows.net/{queueName}");
        return new QueueClient(queueStorageUri, identity);
    }

    public StorageQueueClientFactory(IOptions<AzureConfiguration> options, ILogger<StorageQueueClientFactory> logger)
    {
        _accountName = options.Value.StorageAccount;
    }
}