namespace Objectivity.Bot.Plugins.Resources.Models
{
    using System;

    [Serializable]
    public class EmbeddedResource
    {
        public EmbeddedResource(string assemblyName, string resourceName, string resourceCategory)
        {
            this.AssemblyName = assemblyName;
            this.ResourceName = resourceName;
            this.ResourceCategory = resourceCategory;
        }

        public string AssemblyName { get; }

        public string ResourceName { get; }

        public string ResourceCategory { get; }
    }
}
