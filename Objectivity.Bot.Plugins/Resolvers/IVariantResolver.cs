namespace Objectivity.Bot.Plugins.Resolvers
{
    public interface IVariantResolver<out TVariant>
        where TVariant : IVariant
    {
        TVariant Resolve();
    }
}