namespace Autofac.Variants.Resources.Models
{
    using System;
    using System.Resources;
    using Extensions;

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
    }
}
