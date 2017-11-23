namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Models;
    using Settings;

    [Serializable]
    internal class EmbeddedResourceResolver
    {
        private readonly List<EmbeddedResource> embeddedResources;
        private readonly ISettings settings;

        public EmbeddedResourceResolver(ISettings settings)
        {
            this.settings = settings;

            var resourcesProvider = new EmbeddedResourcesProvider(settings);

            this.embeddedResources = resourcesProvider.GetAssembliesResources();
        }

        public EmbeddedResource ResolveEmbeddedResource(string resourceName)
        {
            var resources = this.embeddedResources
                .Where(resource => resource.ResourceName.Equals(resourceName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!resources.Any())
            {
                throw new ResourceNotFoundException(resourceName, this.settings.VariantId);
            }

            var variantResources = resources.Where(r => r.VariantName == this.settings.VariantId).ToList();

            if (variantResources.Count > 1)
            {
                throw new AmbiguousResourceException(resourceName, this.settings.VariantId);
            }

            return variantResources.SingleOrDefault();
        }

        public EmbeddedResource ResolveDefaultEmbeddedResource(string resourceName)
        {
            var defaultResources = this.embeddedResources
                .Where(resource => resource.AssemblyName.Equals(this.settings.DefaultVariantAssemblyName) &&
                                   resource.ResourceName.Equals(resourceName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!defaultResources.Any())
            {
                throw new ResourceNotFoundException(resourceName);
            }

            if (defaultResources.Count > 1)
            {
                throw new AmbiguousResourceException(resourceName);
            }

            return defaultResources.Single();
        }
    }
}
