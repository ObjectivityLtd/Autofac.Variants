namespace Autofac.Variants.Resolvers
{
    public interface IVariantResolver<out TVariant>
        where TVariant : IVariant
    {
        TVariant Resolve();
    }
}