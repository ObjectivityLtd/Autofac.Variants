namespace Autofac.Variants.Settings
{
    public interface ISettings
    {
        string VariantId { get; }

        string DefaultVariantAssemblyName { get; }
    }
}
