using MyPortfolio.Infrastructure.Plugins.Models;

namespace MyPortfolio.Infrastructure.Plugins.Interfaces
{
    public interface IPluginLoader
    {
        List<ProjectPlugin> LoadPlugins();
    }
}