namespace MyPortfolio.Infrastructure.Plugins.Interfaces
{
    public interface IPluginStorage
    {
        string StorePlugin(string pluginName, string serializedContent);
        
        List<string> GetAllProjectPluginPaths();
        
        string GetProjectDirectoryPath();
        
        string GetPersonalInfoDirectoryPath();
    }
} 