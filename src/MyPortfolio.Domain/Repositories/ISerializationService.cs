namespace MyPortfolio.Domain.Repositories
{
    public interface ISerializationService
    {
        string Serialize<T>(T data);
        T? Deserialize<T>(string data);
    }
} 