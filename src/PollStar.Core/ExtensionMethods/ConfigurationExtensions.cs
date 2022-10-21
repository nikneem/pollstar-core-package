using Microsoft.Extensions.Configuration;

namespace PollStar.Core.ExtensionMethods;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Get a setting or throw an exception if it's not available.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="key">The settings key.</param>
    /// <param name="description">Optional description of what the setting is for.</param>
    /// <returns>The setting.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the setting is not specified, empty or contains only whitespace.</exception>
    public static string GetRequiredValue(this IConfiguration configuration, string key, string? description = null)
    {
        var stringValue = configuration.GetValue<string>(key);

        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new InvalidOperationException(
                $"Missing setting {(description != null ? "for " + description : "")} : {key}");
        }

        return stringValue;
    }
}