namespace Objectivity.Bot.Plugins.Resources
{
    using System.Resources;

    public interface IResourceValuesProvider
    {
        ResourceManager GetResourceManager(string resourceCategory);
    }
}
