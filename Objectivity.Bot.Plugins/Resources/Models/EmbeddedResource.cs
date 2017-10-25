namespace Objectivity.Bot.Plugins.Resources.Models
{
    using System;

    [Serializable]
    public class EmbeddedResource
    {
        public EmbeddedResource(string assemblyName, string resourceName)
        {
            this.AssemblyName = assemblyName;
            this.ResourceName = resourceName;
        }

        public string AssemblyName { get; }

        public string ResourceName { get; }
    }
}
