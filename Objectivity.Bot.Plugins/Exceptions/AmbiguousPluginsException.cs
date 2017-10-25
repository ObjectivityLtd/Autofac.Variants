namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class AmbiguousPluginsException : Exception
    {
        private const string AmbiguousPluginsMessageFormat =
            "Error while resolving plugin type '{0}': more than one type found.";

        private const string AmbiguousPluginsForTenantMessageFormat =
            "Error while resolving plugin type '{0}' for tenant '{1}': more than one type found.";

        public AmbiguousPluginsException()
        {
        }

        public AmbiguousPluginsException(string pluginName)
            : base(GetMessage(pluginName))
        {
        }

        public AmbiguousPluginsException(string pluginName, string tenantName)
            : base(GetMessage(pluginName, tenantName))
        {
        }

        public AmbiguousPluginsException(string pluginName, Exception innerException)
            : base(GetMessage(pluginName), innerException)
        {
        }

        public AmbiguousPluginsException(string pluginName, string tenantName, Exception innerException)
            : base(GetMessage(pluginName, tenantName), innerException)
        {
        }

        protected AmbiguousPluginsException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string pluginName, string tenantName = null)
        {
            if (string.IsNullOrEmpty(tenantName))
            {
                return string.Format(CultureInfo.CurrentCulture, AmbiguousPluginsMessageFormat, pluginName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                AmbiguousPluginsForTenantMessageFormat,
                pluginName,
                tenantName);
        }
    }
}
