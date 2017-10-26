namespace Objectivity.Bot.Plugins.Tests.Resources
{
    using Extensions;
    using Plugins.Resources.Models;
    using Xunit;

    public class EmbeddedResourceExtensionsTests
    {
        private const string ResourceExtension = ".resources";

        [Fact]
        public void GetResourceManager_AssemblyNameIsEmpty_ReturnsNull()
        {
            var resourceName = "Resource";
            var assemblyName = "Test";
            var resourceAssemblyPath = $"{resourceName}.{assemblyName}.{ResourceExtension}";
            var embeddedResource = new EmbeddedResource
            {
                ResourceAssemblyPath = resourceAssemblyPath,
                ResourceName = resourceName
            };

            var resourceManager = embeddedResource.GetResourceManager();

            Assert.Null(resourceManager);
        }

        [Fact]
        public void GetResourceManager_ResourceAssemblyPathIsEmpty_ReturnsNull()
        {
            var resourceName = "Resource";
            var assemblyName = "Test";

            var embeddedResource = new EmbeddedResource
            {
                AssemblyName = assemblyName,
                ResourceName = resourceName
            };

            var resourceManager = embeddedResource.GetResourceManager();

            Assert.Null(resourceManager);
        }
    }
}
