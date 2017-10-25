namespace Objectivity.Bot.Plugins.Providers
{
    using System.Collections.Generic;
    using Objectivity.Bot.Plugins.Resources.Models;

    public interface IResourcesProvider : IPluginType
    {
        string TenantName { get; }

        List<EmbeddedResource> EmbeddedResources { get; }
    }
}
