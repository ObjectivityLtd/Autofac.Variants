namespace Autofac.Variants.Tests.Resources
{
    using Extensions;
    using Variants.Resources.Models;
    using Xunit;

    public class EmbeddedResourceExtensionsTests
    {
        [Fact]
        public void EmptyAssemblyName_GetResourceManagerIsCalled_NullIsReturned()
        {
            var resourceName = "Resource";
            var assemblyName = "Test";
            var resourceAssemblyPath = $"{resourceName}.{assemblyName}.{Constants.ResourceExtension}";
            var embeddedResource = new EmbeddedResource
            {
                ResourceAssemblyPath = resourceAssemblyPath,
                ResourceName = resourceName
            };

            var resourceManager = embeddedResource.GetResourceManager();

            Assert.Null(resourceManager);
        }

        [Fact]
        public void EmptyResourceAssemblyPath_GetResourceManagerIsCalled_NullIsReturned()
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
