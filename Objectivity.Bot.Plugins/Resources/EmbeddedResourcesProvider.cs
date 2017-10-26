namespace Objectivity.Bot.Plugins.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Models;
    using Settings;

    public class EmbeddedResourcesProvider
    {
        private const string ResourceExtension = ".resources";

        private readonly ISettings settings;

        public EmbeddedResourcesProvider(ISettings settings)
        {
            this.settings = settings;
        }

        public static string GetResourceName(string resourceAssemblyPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(resourceAssemblyPath);

            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            return fileName.Split('.').Last();
        }

        public List<EmbeddedResource> GetAssembliesResources()
        {
            var resources = new List<EmbeddedResource>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;

                if (assemblyName.StartsWith(
                    this.settings.DefaultVariantAssemblyName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    var assemblyResources = this.GetAssemblyResources(assembly);

                    resources.AddRange(assemblyResources);
                }
            }

            return resources;
        }

        public List<EmbeddedResource> GetAssemblyResources(_Assembly assembly)
        {
            if (assembly == null)
            {
                return new List<EmbeddedResource>();
            }

            var assemblyName = assembly.GetName().Name;
            var assemblyResourceNames = assembly.GetManifestResourceNames();
            var variantName = assemblyName
                .Replace(this.settings.DefaultVariantAssemblyName, string.Empty)
                .TrimStart('.');

            return assemblyResourceNames
                .Where(this.HasResourceExtension)
                .Select(resourceAssemblyPath => new EmbeddedResource
                {
                    AssemblyName = assemblyName,
                    VariantName = variantName,
                    ResourceAssemblyPath = resourceAssemblyPath,
                    ResourceName = GetResourceName(resourceAssemblyPath)
                })
                .ToList();
        }

        public bool HasResourceExtension(string assemblyResourceName)
        {
            return Path.HasExtension(assemblyResourceName) && string.Equals(
                       ResourceExtension,
                       Path.GetExtension(assemblyResourceName),
                       StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
