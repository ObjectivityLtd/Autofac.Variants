namespace Objectivity.Bot.Plugins.Tests.Resources
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Moq;
    using Plugins.Resources;
    using Plugins.Resources.Models;
    using Settings;
    using Xunit;

    public class EmbeddedResourcesProviderTests
    {
        private const string ResourceExtension = ".resources";
        private const string VariantAssemblyName = "TestAssembly";
        private const string VariantId = "TestVariant";

        [Fact]
        public void HasResourceExtension_ResourceWithoutExtension_ReturnsFalse()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assemblyResourceName = $"{VariantAssemblyName}.{VariantId}.Resource";
            var result = provider.HasResourceExtension(assemblyResourceName);

            Assert.False(result);
        }

        [Fact]
        public void HasResourceExtension_ResourceWithNotCorrectExtension_ReturnsFalse()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assemblyResourceName = $"{VariantAssemblyName}.{VariantId}.Resource.txt";
            var result = provider.HasResourceExtension(assemblyResourceName);

            Assert.False(result);
        }

        [Theory]
        [InlineData(ResourceExtension)]
        [InlineData(".RESOURCES")]
        public void HasResourceExtension_ResourceWithCorrectExtension_ReturnsTrue(string extension)
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assemblyResourceName = $"{VariantAssemblyName}.{VariantId}.Resource{extension}";
            var result = provider.HasResourceExtension(assemblyResourceName);

            Assert.True(result);
        }

        [Fact]
        public void GetResourceName_EmptyResourceAssemblyPath_ReturnsEmptyString()
        {
            var result = EmbeddedResourcesProvider.GetResourceName(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void GetResourceName_ResourceAssemblyPathWithoutNamespace_ReturnsCorrectResourceName()
        {
            var resourceName = "Messages";
            var resourceAssemblyPath = $"{resourceName}{ResourceExtension}";
            var result = EmbeddedResourcesProvider.GetResourceName(resourceAssemblyPath);

            Assert.NotEmpty(result);
            Assert.Equal(resourceName, result);
        }

        [Fact]
        public void GetResourceName_TypicalResourceAssemblyPath_ReturnsCorrectResourceName()
        {
            var resourceName = "Messages";
            var resourceAssemblyPath = $"{VariantAssemblyName}.{VariantId}.{resourceName}{ResourceExtension}";
            var result = EmbeddedResourcesProvider.GetResourceName(resourceAssemblyPath);

            Assert.NotEmpty(result);
            Assert.Equal(resourceName, result);
        }

        [Fact]
        public void GetAssemblyResources_NullAssembly_ReturnsEmptyCollection()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var result = provider.GetAssemblyResources(null);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAssemblyResources_AssemblyWithoutResources_ReturnsEmptyCollection()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assembly = this.GetAssembly($"{VariantAssemblyName}.{VariantId}");
            var result = provider.GetAssemblyResources(assembly);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAssemblyResources_AssemblyWithOneResource_ReturnsOneEmbeddedResourceWithCorrectProperties()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assemblyName = $"{VariantAssemblyName}.{VariantId}";
            var resourceName = "Messages";
            var resourceAssemblyPath = $"{assemblyName}.{resourceName}{ResourceExtension}";
            var assembly = this.GetAssembly(assemblyName, resourceAssemblyPath);
            var result = provider.GetAssemblyResources(assembly);

            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            var resource = result.Single();

            AssertEmbeddedResource(resource, assemblyName, resourceName, resourceAssemblyPath, VariantId);
        }

        [Fact]
        public void GetAssemblyResources_DefaultAssemblyWith1Resource_Returns1EmbeddedResourceWithCorrectProperties()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assemblyName = $"{VariantAssemblyName}";
            var resourceName = "Messages";
            var resourceAssemblyPath = $"{assemblyName}.{resourceName}{ResourceExtension}";
            var assembly = this.GetAssembly(assemblyName, resourceAssemblyPath);
            var result = provider.GetAssemblyResources(assembly);

            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            var resource = result.Single();

            AssertEmbeddedResource(resource, assemblyName, resourceName, resourceAssemblyPath, string.Empty);
        }

        [Fact]
        public void GetAssemblyResources_AssemblyWith10Resources_Returns10EmbeddedResourceWithCorrectProperties()
        {
            var provider = this.GetEmbeddedResourcesProvider();
            var assemblyName = $"{VariantAssemblyName}.{VariantId}";
            var resourceName = "Messages";
            var resourceAssemblyPaths = new List<string>();

            for (var i = 1; i <= 10; i++)
            {
                var resourceAssemblyPath = $"{assemblyName}.{resourceName}{ResourceExtension}";
                resourceAssemblyPaths.Add(resourceAssemblyPath);
            }

            var assembly = this.GetAssembly(assemblyName, resourceAssemblyPaths.ToArray());
            var result = provider.GetAssemblyResources(assembly);

            Assert.NotNull(result);
            Assert.True(result.Count == 10);

            for (var i = 1; i <= 10; i++)
            {
                var resource = result[i - 1];
                var resourceAssemblyPath = $"{assemblyName}.{resourceName}{ResourceExtension}";

                AssertEmbeddedResource(resource, assemblyName, resourceName, resourceAssemblyPath, VariantId);
            }
        }

        private static void AssertEmbeddedResource(EmbeddedResource resource, string assemblyName, string resourceName, string resourceAssemblyPath, string variantId)
        {
            Assert.Equal(assemblyName, resource.AssemblyName);
            Assert.Equal(resourceAssemblyPath, resource.ResourceAssemblyPath);
            Assert.Equal(resourceName, resource.ResourceName);
            Assert.Equal(variantId, resource.VariantName);
        }

        private EmbeddedResourcesProvider GetEmbeddedResourcesProvider()
        {
            var settingsMock = new Mock<ISettings>();
            settingsMock.SetupGet(s => s.DefaultVariantAssemblyName).Returns(VariantAssemblyName);
            settingsMock.SetupGet(s => s.VariantId).Returns(VariantId);

            return new EmbeddedResourcesProvider(settingsMock.Object);
        }

        private _Assembly GetAssembly(string name, params string[] embeddedResourceNames)
        {
            var assemblyMock = new Mock<_Assembly>();
            var assemblyName = new AssemblyName(name);
            assemblyMock.Setup(a => a.GetManifestResourceNames()).Returns(embeddedResourceNames);
            assemblyMock.Setup(a => a.GetName()).Returns(assemblyName);

            return assemblyMock.Object;
        }
    }
}
