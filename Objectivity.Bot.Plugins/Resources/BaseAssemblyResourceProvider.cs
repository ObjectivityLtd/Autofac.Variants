namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    [Serializable]
    public abstract class BaseAssemblyResourceProvider : IAssemblyResourceProvider
    {
        public const string ResourceExtension = ".resources";

        private List<EmbeddedResource> assemblyResources;

        public abstract string TenantName { get; }

        public List<EmbeddedResource> EmbeddedResources
        {
            get
            {
                if (this.assemblyResources == null)
                {
                    this.assemblyResources = this.GetAssemblyResources();
                }

                return this.assemblyResources;
            }
        }

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

        public List<EmbeddedResource> GetAssemblyResources()
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
