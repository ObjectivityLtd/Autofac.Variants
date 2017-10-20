namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using Settings;

    [Serializable]
    public class ResourceValuesProvider : IResourceValuesProvider
    {
        public const string ResourceExtension = ".resources";

        private readonly IEnumerable<IAssemblyResourceProvider> resourceProviders;
        private readonly ITenancySettings tenancySettings;

        public ResourceValuesProvider(
            IEnumerable<IAssemblyResourceProvider> resourceProviders,
            ITenancySettings tenancySettings)
        {
            this.resourceProviders = resourceProviders;
            this.tenancySettings = tenancySettings;
        }

        public ResourceManager GetResourceManager(string resourceCategory)
        {
            var resource = this.ResolveEmbeddedResource(resourceCategory);
            var resourceAssembly = GetResourceAssembly(resource);
            var resourceName = resource.ResourceName.Replace(ResourceExtension, string.Empty);

            return new ResourceManager(resourceName, resourceAssembly);
        }

        private static Assembly GetResourceAssembly(EmbeddedResource resource)
        {
            var resourceAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => assembly.GetName().Name == resource.AssemblyName);

            if (resourceAssembly == null)
            {
                throw new ArgumentException($"Couldn't find assembly named {resource.AssemblyName}.");
            }

            return resourceAssembly;
        }

        private EmbeddedResource ResolveEmbeddedResource(string resourceCategory)
        {
            var embeddedResourceProvider = this.resourceProviders
                .Where(resourcesProvider => resourcesProvider.EmbeddedResources
                    .Any(embeddedResource => embeddedResource.ResourceCategory == resourceCategory))
                .OrderByDescending(provider => provider.TenantName == this.tenancySettings.TenantName)
                .FirstOrDefault();

            if (embeddedResourceProvider == null)
            {
                throw new MissingMemberException(
                    $"Couldn't resolve any Embedded Resources Provider for Resource Category = {resourceCategory}.");
            }

            return embeddedResourceProvider.EmbeddedResources
                .Single(embeddedResource => embeddedResource.ResourceCategory == resourceCategory);
        }
    }
}
