namespace Objectivity.Bot.Plugins
{
    using System;
    using System.Linq;
    using Objectivity.Bot.Plugins.Settings;

    internal static class TenantResolver
    {
        public static string GetTenantName(this object obj, ITenancySettings tenancySettings)
        {
            var typeNamespace = obj.GetType().Namespace;

            if (string.IsNullOrEmpty(typeNamespace))
            {
                return string.Empty;
            }

            if (!typeNamespace.StartsWith(
                tenancySettings.PluginAssemblyNamePrefix,
                StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return typeNamespace.Split('.').Last();
        }

        public static bool IsMatchingTenant(this object obj, ITenancySettings tenancySettings)
        {
            var tenantName = obj.GetTenantName(tenancySettings);

            return string.Equals(tenancySettings.TenantName, tenantName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
