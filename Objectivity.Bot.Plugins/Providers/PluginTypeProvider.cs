namespace Objectivity.Bot.Plugins.Providers
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Settings;

    public class PluginTypeProvider<T> : IPluginTypeProvider<T>
        where T : IPluginType
    {
        private readonly ITenancySettings tenancySettings;
        private readonly IEnumerable<T> pluginTypes;

        public PluginTypeProvider(ITenancySettings tenancySettings, IEnumerable<T> pluginTypes)
        {
            this.tenancySettings = tenancySettings;
            this.pluginTypes = pluginTypes;
        }

        public T GetDefaultPluginType()
        {
            return this.GePluginTypeFor(this.tenancySettings.TenantName);
        }

        public T GePluginTypeFor(string tenantName)
        {
            var pluginTypesList = this.pluginTypes.ToList();

            if (!pluginTypesList.Any())
            {
                throw new PluginNotFoundException(typeof(T).Name);
            }

            return this.pluginTypes.FirstOrDefault(dialog => dialog.TenantName == tenantName);
        }
    }
}