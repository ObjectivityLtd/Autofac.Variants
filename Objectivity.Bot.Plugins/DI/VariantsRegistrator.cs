﻿namespace Objectivity.Bot.Plugins.DI
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using Autofac;
    using Autofac.Integration.Mef;
    using Exceptions;
    using Providers;
    using Resources;
    using Settings;

    public static class VariantsRegistrator
    {
        public static void RegisterVariants(ContainerBuilder builder, ISettings settings)
        {
            if (string.IsNullOrEmpty(settings?.VariantId))
            {
                throw new EmptyVariantIdException();
            }

            builder.RegisterInstance(settings).As<ISettings>().ExternallyOwned();

            builder.RegisterType<ResourcesManager>().As<IResourcesManager>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith(
                    settings.DefaultVariantAssemblyName,
                    StringComparison.OrdinalIgnoreCase));

            var assembliesCatalogs = assemblies.Select(a => new AssemblyCatalog(a));

            using (var aggregateCatalog = new AggregateCatalog(assembliesCatalogs))
            {
                builder.RegisterComposablePartCatalog(aggregateCatalog);
            }

            builder.RegisterGeneric(typeof(VariantResolver<>)).As(typeof(IVariantResolver<>))
                .InstancePerDependency();
        }
    }
}