namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class PluginNotFoundException : Exception
    {
        private const string PluginNotFoundMessageFormat = "Couldn't find any plugins of type '{0}'.";

        private const string PluginNotFoundForTenantMessageFormat = "Couldn't find any plugins of type '{0}' for tenant '{1}'.";

        public PluginNotFoundException()
        {
        }

        public PluginNotFoundException(string pluginName)
            : base(GetMessage(pluginName))
        {
        }

        public PluginNotFoundException(string pluginName, string tenantName)
            : base(GetMessage(pluginName, tenantName))
        {
        }

        public PluginNotFoundException(string pluginName, Exception innerException)
            : base(GetMessage(pluginName), innerException)
        {
        }

        public PluginNotFoundException(string pluginName, string tenantName, Exception innerException)
            : base(GetMessage(pluginName, tenantName), innerException)
        {
        }

        protected PluginNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string pluginName, string tenantName = null)
        {
            if (string.IsNullOrEmpty(tenantName))
            {
                return string.Format(CultureInfo.CurrentCulture, PluginNotFoundMessageFormat, pluginName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                PluginNotFoundForTenantMessageFormat,
                pluginName,
                tenantName);
        }
    }
}
