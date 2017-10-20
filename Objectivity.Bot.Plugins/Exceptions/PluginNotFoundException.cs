namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PluginNotFoundException : Exception
    {
        private const string PluginNotFoundMessageFormat = "Couldn't find any plugins of type '{0}'.";

        public PluginNotFoundException()
        {
        }

        public PluginNotFoundException(string pluginName)
            :base(GetMessage(pluginName))
        {
        }

        public PluginNotFoundException(string pluginName, Exception innerException)
            : base(GetMessage(pluginName), innerException)
        {
        }

        protected PluginNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string pluginName)
        {
            return string.Format(PluginNotFoundMessageFormat, pluginName);
        }
    }
}
