namespace Objectivity.Bot.Plugins.Providers
{
    public interface IVariantResolver<out TVariant>
        where TVariant : IVariant
    {
        TVariant Resolve();
    }
}