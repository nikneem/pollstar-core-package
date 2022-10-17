using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PollStar.Core.Configuration;

namespace PollStar.Core.Factories;

public class ServiceBusClientFactory: IServiceBusClientFactory
{

    private readonly IOptions<AzureConfiguration> _options;
    private readonly Lazy<ServiceBusClient> _client;

    public ServiceBusSender CreateSender(string queueName)
    {
        return _client.Value.CreateSender(queueName);
    }
    public ServiceBusReceiver CreateReceiver(string queueName)
    {
        return _client.Value.CreateReceiver(queueName);
    }

    private ServiceBusClient CreateClient()
    {
        var credential = new DefaultAzureCredential();
        return new ServiceBusClient(_options.Value.ServiceBus, credential);
    }

    public ServiceBusClientFactory(IOptions<AzureConfiguration> options, ILogger<StorageQueueClientFactory> logger)
    {
        _options = options;
        _client = new Lazy<ServiceBusClient>(CreateClient);
    }
}