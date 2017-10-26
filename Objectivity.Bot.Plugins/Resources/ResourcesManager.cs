namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using Exceptions;
    using Settings;

    [Serializable]
    public class ResourcesManager : IResourcesManager
    {
        private readonly EmbeddedResourceResolver resourcesResolver;

        public ResourcesManager(ISettings settings)
        {
            this.resourcesResolver = new EmbeddedResourceResolver(settings);
        }

        public string GetString(string key, string resourceName)
        {
            var embeddedResource = this.resourcesResolver.ResolveEmbeddedResource(resourceName);

            if (embeddedResource == null || !embeddedResource.ContainsKey(key))
            {
                embeddedResource = this.resourcesResolver.ResolveDefaultEmbeddedResource(resourceName);
            }

            if (!embeddedResource.ContainsKey(key))
            {
                throw new ResourceKeyNotFoundException(key, resourceName);
            }

            return embeddedResource.GetString(key);
        }
    }
}
