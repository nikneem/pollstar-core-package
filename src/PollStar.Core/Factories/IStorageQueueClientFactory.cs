using Azure.Storage.Queues;

namespace PollStar.Core.Factories;

public interface IStorageQueueClientFactory
{
    QueueClient CreateClient(string queueName);
}