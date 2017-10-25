namespace Objectivity.Bot.Plugins.Providers
{
    using System.Collections.Generic;
    using Resources.Models;

    public interface IResourcesVariant : IVariant
    {
        List<EmbeddedResource> EmbeddedResources { get; }
    }
}
