using System.Collections.Generic;

namespace MyPortfolio.Domain.Interfaces
{
    public interface IPluginLoader
    {
        List<IPlugin> LoadPlugins(string directory);
        IPlugin? LoadPlugin(string filePath);
    }
} 