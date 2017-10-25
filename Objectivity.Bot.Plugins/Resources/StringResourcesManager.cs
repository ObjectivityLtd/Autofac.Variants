namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using Objectivity.Bot.Plugins.Providers;

    [Serializable]
    public class StringResourcesManager : IResourcesManager
    {
        private readonly ResourceManagersFactory resourceManagersFactory;

        public StringResourcesManager(IPluginTypeProvider<IResourcesProvider> resourcesProviders)
        {
            this.resourceManagersFactory = new ResourceManagersFactory(resourcesProviders);
        }

        public string GetString(string key, string resourceCategory)
        {
            var resourceManager = this.resourceManagersFactory.GetResourceManager(resourceCategory);

            return resourceManager.GetString(key);
        }
    }
}
