namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Resources;

    [Serializable]
    public class StringResourcesManager : IResourcesManager
    {
        private readonly IResourceValuesProvider resourceValuesProvider;

        private readonly Dictionary<string, ResourceManager> resourceManagers =
            new Dictionary<string, ResourceManager>();

        public StringResourcesManager(IResourceValuesProvider resourceValuesProvider)
        {
            this.resourceValuesProvider = resourceValuesProvider;
        }

        public string GetString(string key, string resourceCategory)
        {
            var resourceManager = this.GetResourceManager(resourceCategory);

            return resourceManager.GetString(key);
        }

        private ResourceManager GetResourceManager(string resourceKey)
        {
            if (!this.resourceManagers.ContainsKey(resourceKey))
            {
                var resourceManager = this.resourceValuesProvider.GetResourceManager(resourceKey);

                this.resourceManagers.Add(resourceKey, resourceManager);
            }

            return this.resourceManagers[resourceKey];
        }
    }
}
