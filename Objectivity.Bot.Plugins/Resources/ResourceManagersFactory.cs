namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using Models;
    using Providers;

    [Serializable]
    internal class ResourceManagersFactory
    {
        private readonly IVariantResolver<IResourcesVariant> resourcesProviders;

        public ResourceManagersFactory(IVariantResolver<IResourcesVariant> resourcesProviders)
        {
            this.resourcesProviders = resourcesProviders;
        }

        public ResourceManager GetResourceManager(string resourceCategory)
        {
            var resource = this.ResolveEmbeddedResource(resourceCategory);
            var resourceAssembly = GetResourceAssembly(resource);

            return new ResourceManager(resource.ResourceName, resourceAssembly);
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

        private EmbeddedResource ResolveEmbeddedResource(string resourceName)
        {
            var resourcesProvider = this.resourcesProviders.Resolve();

            if (resourcesProvider == null)
            {
                var exceptionMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "Couldn't resolve any Embedded Resources Provider for Resource Name: '{0}'.",
                    resourceName);

                // TODO: throw custom exception
                throw new MissingMemberException(exceptionMessage);
            }

            // TODO: throw new custom exception saying there is no such resource

            return resourcesProvider.EmbeddedResources
                .Single(embeddedResource =>
                    embeddedResource.ResourceName.Equals(resourceName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
