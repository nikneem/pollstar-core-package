﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace PollStar.Core.ExtensionMethods;

public static class ServiceBusMessageExtensions
{
    public static ServiceBusMessage ToServiceBusMessage(this object payload, string? correlationId = null)
    {
        var payloadJson = JsonConvert.SerializeObject(payload);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(payloadJson))
        {
            CorrelationId = correlationId ?? Guid.NewGuid().ToString()
        };
        return message;
    }
    
}