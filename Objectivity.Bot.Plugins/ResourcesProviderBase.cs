namespace Objectivity.Bot.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Objectivity.Bot.Plugins.Providers;
    using Objectivity.Bot.Plugins.Resources.Models;
    using Objectivity.Bot.Plugins.Settings;

    [Serializable]
    public abstract class ResourcesProviderBase : IResourcesProvider
    {
        public const string ResourceExtension = ".resources";

        private List<EmbeddedResource> assemblyResources;

        protected ResourcesProviderBase(ITenancySettings tenancySettings)
        {
            this.TenantName = this.GetTenantName(tenancySettings);
        }

        public string TenantName { get; }

        public List<EmbeddedResource> EmbeddedResources =>
            this.assemblyResources ?? (this.assemblyResources = this.GetAssemblyResources());

        public static EmbeddedResource GetEmbeddedResource(string assemblyName, string resourceName)
        {
            resourceName = Path.GetFileNameWithoutExtension(resourceName);

            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            var categoryName = resourceName.Split('.').Last();

            return new EmbeddedResource(assemblyName, resourceName, categoryName);
        }

        private List<EmbeddedResource> GetAssemblyResources()
        {
            var assembly = this.GetType().Assembly;
            var assemblyName = assembly.GetName().Name;
            var embeddedResources = assembly.GetManifestResourceNames();

            return embeddedResources.Where(this.HasResourceExtension)
                .Select(filename => GetEmbeddedResource(assemblyName, filename))
                .ToList();
        }

        private bool HasResourceExtension(string filename)
        {
            return Path.HasExtension(filename) && string.Equals(
                       ResourceExtension,
                       Path.GetExtension(filename),
                       StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
