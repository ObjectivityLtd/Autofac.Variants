namespace Objectivity.Bot.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Providers;
    using Resources.Models;

    public abstract class ResourcesVariantBase : IResourcesVariant
    {
        public const string ResourceExtension = ".resources";

        private List<EmbeddedResource> assemblyResources;

        public List<EmbeddedResource> EmbeddedResources =>
            this.assemblyResources ?? (this.assemblyResources = this.GetAssemblyResources());

        private static EmbeddedResource GetEmbeddedResource(string assemblyName, string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);

            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var resourceName = fileName.Split('.').Last();

            return new EmbeddedResource(assemblyName, resourceName);
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
