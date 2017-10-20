namespace Objectivity.Bot.Plugins.Resources
{
    using System.Collections.Generic;

    public interface IAssemblyResourceProvider
    {
        string TenantName { get; }

        List<EmbeddedResource> EmbeddedResources { get; }
    }
}
