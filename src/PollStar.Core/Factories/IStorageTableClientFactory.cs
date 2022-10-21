using Azure.Data.Tables;

namespace PollStar.Core.Factories;

public interface IStorageTableClientFactory
{
    TableClient CreateClient(string tableName);
}