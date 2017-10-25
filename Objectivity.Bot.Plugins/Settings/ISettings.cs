namespace Objectivity.Bot.Plugins.Settings
{
    public interface ISettings
    {
        string VariantId { get; }

        string DefaultVariantAssemblyName { get; }
    }
}
