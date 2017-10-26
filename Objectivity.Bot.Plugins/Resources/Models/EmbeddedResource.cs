namespace Objectivity.Bot.Plugins.Resources.Models
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    [Serializable]
    public class EmbeddedResource
    {
        private ResourceManager resourceManager;

        public string AssemblyName { get; set; }

        public string ResourceAssemblyPath { get; set; }

        public string VariantName { get; set; }

        public string ResourceName { get; set; }

        public ResourceManager ResourceManager => this.resourceManager ?? (this.resourceManager = this.GetResourceManager());

        public string GetString(string key)
        {
            return this.ResourceManager?.GetString(key);
        }

        public bool ContainsKey(string key)
        {
            var value = this.GetString(key);

            return !string.IsNullOrEmpty(value);
        }

        private ResourceManager GetResourceManager()
        {
            if (string.IsNullOrEmpty(this.AssemblyName) || string.IsNullOrEmpty(this.ResourceAssemblyPath))
            {
                return null;
            }

            var baseName = Path.GetFileNameWithoutExtension(this.ResourceAssemblyPath);

            if (string.IsNullOrEmpty(baseName))
            {
                return null;
            }

            var resourceAssembly = this.GetResourceAssembly();

            return new ResourceManager(baseName, resourceAssembly);
        }

        private Assembly GetResourceAssembly()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var resourceAssembly = assemblies
                .FirstOrDefault(assembly => assembly.GetName().Name == this.AssemblyName);

            if (resourceAssembly == null)
            {
                var exceptionMessage = string.Format(
                    CultureInfo.CurrentCulture,
                    "Couldn't find assembly named {0}.",
                    this.AssemblyName);

                throw new ArgumentException(exceptionMessage);
            }

            return resourceAssembly;
        }
    }
}
