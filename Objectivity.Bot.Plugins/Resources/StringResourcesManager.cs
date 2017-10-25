namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using Providers;

    [Serializable]
    public class StringResourcesManager : IResourcesManager
    {
        private readonly ResourceManagersFactory resourceManagersFactory;

        public StringResourcesManager(IVariantResolver<IResourcesVariant> resourcesProviders)
        {
            this.resourceManagersFactory = new ResourceManagersFactory(resourcesProviders);
        }

        public string GetString(string key, string resourceName)
        {
            var resourceManager = this.resourceManagersFactory.GetResourceManager(resourceName);

            return resourceManager.GetString(key);
        }
    }
}
