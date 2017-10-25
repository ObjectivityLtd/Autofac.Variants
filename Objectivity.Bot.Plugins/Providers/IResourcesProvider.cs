namespace Objectivity.Bot.Plugins.Providers
{
    using System.Collections.Generic;
    using Objectivity.Bot.Plugins.Resources.Models;

    public interface IResourcesProvider : IPluginType
    {
        List<EmbeddedResource> EmbeddedResources { get; }
    }
}
