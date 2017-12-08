namespace Autofac.Variants.Tests.Resources
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Moq;
    using Settings;
    using Variants.Resources;
    using Variants.Resources.Models;
    using Xunit;

    public class EmbeddedResourcesProviderTests
    {
        private const string ResourceName = "Messages";
        private const string VariantAssemblyName = "TestAssembly";
        private const string VariantId = "TestVariant";

        [Fact]
        public void ResourceWithoutExtension_HasResourceExtensionIsCalled_FalseIsReturned()
        {
            var provider = GetEmbeddedResourcesProvider();
            var assemblyResourceName = $"{VariantAssemblyName}.{VariantId}.Resource";
            var result = provider.HasResourceExtension(assemblyResourceName);

            Assert.False(result);
        }

        [Fact]
        public void ResourceWithIncorrectExtension_HasResourceExtensionIsCalled_FalseIsReturned()
        {
            var provider = GetEmbeddedResourcesProvider();
            var assemblyResourceName = $"{VariantAssemblyName}.{VariantId}.Resource.txt";
            var result = provider.HasResourceExtension(assemblyResourceName);

            Assert.False(result);
        }

        [Theory]
        [InlineData(Constants.ResourceExtension)]
        [InlineData(".RESOURCES")]
        public void ResourceWithCorrectExtension_HasResourceExtensionIsCalled_TrueIsReturned(string extension)
        {
            var provider = GetEmbeddedResourcesProvider();
            var assemblyResourceName = $"{VariantAssemblyName}.{VariantId}.Resource{extension}";
            var result = provider.HasResourceExtension(assemblyResourceName);

            Assert.True(result);
        }

        [Fact]
        public void ResourceWithEmptyAssemblyPath_GetResourceNameIsCalled_EmptyStringIsReturned()
        {
            var result = EmbeddedResourcesProvider.GetResourceName(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void ResourceWithEmptyAssemblyPathWithoutNamespace_GetResourceNameIsCalled_CorrectResourceNameIsReturned()
        {
            var resourceAssemblyPath = $"{ResourceName}{Constants.ResourceExtension}";
            var result = EmbeddedResourcesProvider.GetResourceName(resourceAssemblyPath);

            Assert.NotEmpty(result);
            Assert.Equal(ResourceName, result);
        }

        [Fact]
        public void ResourceWithNormalAssemblyPath_GetResourceNameIsCalled_CorrectResourceNameIsReturned()
        {
            var resourceAssemblyPath = $"{VariantAssemblyName}.{VariantId}.{ResourceName}{Constants.ResourceExtension}";
            var result = EmbeddedResourcesProvider.GetResourceName(resourceAssemblyPath);

            Assert.NotEmpty(result);
            Assert.Equal(ResourceName, result);
        }

        [Fact]
        public void NullAssembly_GetAssemblyResourcesIsCalled_EmptyCollectionIsReturned()
        {
            var provider = GetEmbeddedResourcesProvider();
            var result = provider.GetAssemblyResources(null);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void AssemblyWithoutResources_GetAssemblyResourcesIsCalled_EmptyCollectionIsReturned()
        {
            var provider = GetEmbeddedResourcesProvider();
            var assembly = GetAssembly($"{VariantAssemblyName}.{VariantId}");
            var result = provider.GetAssemblyResources(assembly);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(10)]
        public void AssemblyWithSomeResources_GetAssemblyResourcesIsCalled_SomeCorrectEmbeddedResourcesAreReturned(int resourcesCount)
        {
            var provider = GetEmbeddedResourcesProvider();
            var assemblyName = $"{VariantAssemblyName}.{VariantId}";
            var resourceAssemblyPaths = new List<string>();

            for (var i = 1; i <= resourcesCount; i++)
            {
                var resourceAssemblyPath = $"{assemblyName}.{ResourceName}{Constants.ResourceExtension}";
                resourceAssemblyPaths.Add(resourceAssemblyPath);
            }

            var assembly = GetAssembly(assemblyName, resourceAssemblyPaths.ToArray());
            var result = provider.GetAssemblyResources(assembly);

            Assert.NotNull(result);
            Assert.True(result.Count == resourcesCount);

            for (var i = 1; i <= resourcesCount; i++)
            {
                var resource = result[i - 1];
                var resourceAssemblyPath = $"{assemblyName}.{ResourceName}{Constants.ResourceExtension}";

                AssertEmbeddedResource(resource, assemblyName, ResourceName, resourceAssemblyPath, VariantId);
            }
        }

        private static void AssertEmbeddedResource(EmbeddedResource resource, string assemblyName, string resourceName, string resourceAssemblyPath, string variantId)
        {
            Assert.Equal(assemblyName, resource.AssemblyName);
            Assert.Equal(resourceAssemblyPath, resource.ResourceAssemblyPath);
            Assert.Equal(resourceName, resource.ResourceName);
            Assert.Equal(variantId, resource.VariantName);
        }

        private static EmbeddedResourcesProvider GetEmbeddedResourcesProvider()
        {
            var settingsMock = new Mock<ISettings>();
            settingsMock.SetupGet(s => s.DefaultVariantAssemblyName).Returns(VariantAssemblyName);
            settingsMock.SetupGet(s => s.VariantId).Returns(VariantId);

            return new EmbeddedResourcesProvider(settingsMock.Object);
        }

        private static _Assembly GetAssembly(string name, params string[] embeddedResourceNames)
        {
            var assemblyMock = new Mock<_Assembly>();
            var assemblyName = new AssemblyName(name);
            assemblyMock.Setup(a => a.GetManifestResourceNames()).Returns(embeddedResourceNames);
            assemblyMock.Setup(a => a.GetName()).Returns(assemblyName);

            return assemblyMock.Object;
        }
    }
}
