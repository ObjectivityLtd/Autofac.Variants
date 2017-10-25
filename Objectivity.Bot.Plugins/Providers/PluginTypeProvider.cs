namespace Objectivity.Bot.Plugins.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Settings;

    public class PluginTypeProvider<TPluginType> : IPluginTypeProvider<TPluginType>
        where TPluginType : IPluginType
    {
        private readonly string pluginName;
        private readonly ITenancySettings tenancySettings;
        private readonly IEnumerable<TPluginType> pluginTypes;

        public PluginTypeProvider(ITenancySettings tenancySettings, IEnumerable<TPluginType> pluginTypes)
        {
            this.pluginName = typeof(TPluginType).Name;
            this.tenancySettings = tenancySettings;
            this.pluginTypes = pluginTypes;
        }

        public TPluginType GetPluginTypeForCurrentTenant()
        {
            return this.GetPluginTypeForTenant(this.tenancySettings.TenantName);
        }

        public TPluginType GetPluginTypeForCurrentTenantOrDefault()
        {
            return this.GetPluginTypeForTenantOrDefault(this.tenancySettings.TenantName);
        }

        public TPluginType GetPluginTypeForTenant(string tenantName)
        {
            return this.GetPluginType(tenantName, false);
        }

        public TPluginType GetPluginTypeForTenantOrDefault(string tenantName)
        {
            return this.GetPluginType(tenantName, true);
        }

        private TPluginType GetPluginType(string tenantName, bool allowDefault)
        {
            var tenantPluginTypesList = this.pluginTypes
                .Where(plugin => plugin.IsMatchingTenant(this.tenancySettings))
                .ToList();

            if (!tenantPluginTypesList.Any() && !allowDefault)
            {
                throw new PluginNotFoundException(this.pluginName, tenantName);
            }

            if (!tenantPluginTypesList.Any())
            {
                return this.GetDefaultPluginType();
            }

                if (tenantPluginTypesList.Count > 1)
            {
                throw new AmbiguousPluginsException(this.pluginName, tenantName);
            }

            return tenantPluginTypesList.Single();
        }

        private TPluginType GetDefaultPluginType()
        {
            var defaultPluginTypesList = this.pluginTypes
                .Where(plugin => plugin is IDefaultPluginType)
                .ToList();

            if (!defaultPluginTypesList.Any())
            {
                throw new DefaultPluginNotFoundException(this.pluginName);
            }

            if (defaultPluginTypesList.Count > 1)
            {
                throw new AmbiguousPluginsException(this.pluginName, "Default");
            }

            return defaultPluginTypesList.Single();
        }
    }
}