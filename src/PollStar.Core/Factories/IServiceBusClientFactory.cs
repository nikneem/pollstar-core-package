using Azure.Messaging.ServiceBus;

namespace PollStar.Core.Factories;

public interface IServiceBusClientFactory
{
    ServiceBusSender CreateSender(string queueName);
    ServiceBusReceiver CreateReceiver(string queueName);
}