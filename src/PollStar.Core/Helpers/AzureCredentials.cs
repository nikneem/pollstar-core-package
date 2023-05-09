using Azure.Core;
using Azure.Identity;

namespace PollStar.Core.Helpers;

public static class AzureCredentials
{
    public static TokenCredential GetAzureCredential()
    {
        return new ChainedTokenCredential(
            new ManagedIdentityCredential(),
            new AzureCliCredential(),
            new VisualStudioCredential(),
            new VisualStudioCodeCredential());
    }
}