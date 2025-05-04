using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPortfolio.Domain.Repositories;

namespace MyPortfolio.Infrastructure.Services
{
    public class JsonSerializationService(
        ILogger<JsonSerializationService> logger
    ) : ISerializationService
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public string Serialize<T>(T data)
        {
            try
            {
                return JsonSerializer.Serialize(data, _options);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error serializing data of type {Type}", typeof(T).Name);
                throw new InvalidOperationException($"Failed to serialize {typeof(T).Name}", ex);
            }
        }

        public T? Deserialize<T>(string data)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(data, _options);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deserializing data to type {Type}", typeof(T).Name);
                throw new InvalidOperationException($"Failed to deserialize to {typeof(T).Name}", ex);
            }
        }
    }
} 