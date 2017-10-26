namespace Objectivity.Bot.Plugins.Extensions
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using Exceptions;
    using Resources.Models;

    public static class EmbeddedResourceExtensions
    {
        internal static ResourceManager GetResourceManager(this EmbeddedResource embeddedResource)
        {
            if (string.IsNullOrEmpty(embeddedResource.AssemblyName) || string.IsNullOrEmpty(embeddedResource.ResourceAssemblyPath))
            {
                return null;
            }

            var baseName = Path.GetFileNameWithoutExtension(embeddedResource.ResourceAssemblyPath);

            if (string.IsNullOrEmpty(baseName))
            {
                return null;
            }

            var resourceAssembly = GetResourceAssembly(embeddedResource.AssemblyName);

            return new ResourceManager(baseName, resourceAssembly);
        }

        private static Assembly GetResourceAssembly(string assemblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var resourceAssembly = assemblies
                .FirstOrDefault(assembly => assembly.GetName().Name == assemblyName);

            if (resourceAssembly == null)
            {
                throw new AssemblyNotFountException(assemblyName);
            }

            return resourceAssembly;
        }
    }
}
