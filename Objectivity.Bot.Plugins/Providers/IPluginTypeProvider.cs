namespace Objectivity.Bot.Plugins.Providers
{
    public interface IPluginTypeProvider<T>
        where T : IPluginType
    {
        T GetPluginTypeForCurrentTenantOrDefault();

        T GetPluginTypeForCurrentTenant();

        T GetPluginTypeForTenant(string tenantName);

        T GetPluginTypeForTenantOrDefault(string tenantName);
    }
}