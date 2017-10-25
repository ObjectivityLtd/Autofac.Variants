namespace Objectivity.Bot.Plugins.DI
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using Autofac;
    using Autofac.Integration.Mef;
    using Providers;
    using Resources;
    using Settings;

    public static class PluginsRegistrator
    {
        public static void RegisterPlugins(ContainerBuilder builder, ITenancySettings tenancySettings)
        {
            builder.RegisterInstance(tenancySettings).As<ITenancySettings>().ExternallyOwned();

            builder.RegisterType<StringResourcesManager>().As<IResourcesManager>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith(
                    tenancySettings.PluginAssemblyNamePrefix,
                    StringComparison.OrdinalIgnoreCase));

            var assembliesCatalogs = assemblies.Select(a => new AssemblyCatalog(a));

            using (var aggregateCatalog = new AggregateCatalog(assembliesCatalogs))
            {
                builder.RegisterComposablePartCatalog(aggregateCatalog);
            }

            builder.RegisterGeneric(typeof(PluginTypeProvider<>)).As(typeof(IPluginTypeProvider<>))
                .InstancePerDependency();
        }
    }
}
