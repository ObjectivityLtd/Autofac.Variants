namespace Autofac.Variants.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Settings;

    public class VariantResolver<TVariant> : IVariantResolver<TVariant>
        where TVariant : IVariant
    {
        private readonly ISettings settings;
        private readonly string variantInterfaceName;
        private readonly IEnumerable<TVariant> variants;

        public VariantResolver(ISettings settings, IEnumerable<TVariant> variants)
        {
            this.settings = settings;
            this.variants = variants;
            this.variantInterfaceName = typeof(TVariant).Name;
        }

        public TVariant Resolve()
        {
            var matchingVariantsList = this.variants

                // ReSharper disable once PossibleNullReferenceException - ensured by namespace convetion
                .Where(variant => variant.GetType().Namespace
                    .EndsWith(this.settings.VariantId, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!matchingVariantsList.Any())
            {
                return this.ResolveDefault();
            }

            if (matchingVariantsList.Count > 1)
            {
                throw new AmbiguousVariantInterfaceException(this.variantInterfaceName, this.settings.VariantId);
            }

            return matchingVariantsList.Single();
        }

        private TVariant ResolveDefault()
        {
            var defaultPluginTypesList = this.variants

                // ReSharper disable once PossibleNullReferenceException - ensured by namespace convetion
                .Where(variant => variant.GetType().Assembly.GetName().Name
                    .Equals(this.settings.DefaultVariantAssemblyName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!defaultPluginTypesList.Any())
            {
                throw new DefaultVariantInterfaceNotFoundException(this.variantInterfaceName);
            }

            if (defaultPluginTypesList.Count > 1)
            {
                throw new AmbiguousVariantInterfaceException(this.variantInterfaceName);
            }

            return defaultPluginTypesList.Single();
        }
    }
}