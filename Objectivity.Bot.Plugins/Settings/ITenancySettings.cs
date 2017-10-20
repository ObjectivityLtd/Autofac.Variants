namespace Objectivity.Bot.Plugins.Settings
{
    public interface ITenancySettings
    {
        string TenantName { get; }

        string PluginAssemblyNamePrefix { get; }
    }
}
