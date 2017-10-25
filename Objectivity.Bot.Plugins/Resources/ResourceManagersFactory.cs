namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using Objectivity.Bot.Plugins.Providers;
    using Objectivity.Bot.Plugins.Resources.Models;

    [Serializable]
    internal class ResourceManagersFactory
    {
        public const string ResourceExtension = ".resources";

        private readonly IPluginTypeProvider<IResourcesProvider> resourcesProviders;

        public ResourceManagersFactory(IPluginTypeProvider<IResourcesProvider> resourcesProviders)
        {
            this.resourcesProviders = resourcesProviders;
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
                var exceptionMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "Couldn't find assembly named {0}.",
                    resource.AssemblyName);

                throw new ArgumentException(exceptionMessage);
            }

            return resourceAssembly;
        }

        private EmbeddedResource ResolveEmbeddedResource(string resourceCategory)
        {
            var resourcesProvider = this.resourcesProviders.GetPluginTypeForCurrentTenantOrDefault();

            if (resourcesProvider == null)
            {
                var exceptionMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "Couldn't resolve any Embedded Resources Provider for Resource Category: '{0}'.",
                    resourceCategory);

                throw new MissingMemberException(exceptionMessage);
            }

            return resourcesProvider.EmbeddedResources
                .Single(embeddedResource => embeddedResource.ResourceCategory == resourceCategory);
        }
    }
}
