namespace Objectivity.Bot.Plugins.Providers
{
    public interface IPluginTypeProvider<T>
        where T : IPluginType
    {
        T GetDefaultPluginType();

        T GePluginTypeFor(string tenantName);
    }
}