namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class DefaultPluginNotFoundException : Exception
    {
        private const string DefaultPluginNotFoundMessageFormat = "Couldn't find default plugin of type '{0}'.";

        public DefaultPluginNotFoundException()
        {
        }

        public DefaultPluginNotFoundException(string pluginName)
            : base(GetMessage(pluginName))
        {
        }

        public DefaultPluginNotFoundException(string pluginName, Exception innerException)
            : base(GetMessage(pluginName), innerException)
        {
        }

        protected DefaultPluginNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string pluginName, string tenantName = null)
        {
            if (string.IsNullOrEmpty(tenantName))
            {
                return string.Format(CultureInfo.CurrentCulture, DefaultPluginNotFoundMessageFormat, pluginName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                DefaultPluginNotFoundMessageFormat,
                pluginName);
        }
    }
}
