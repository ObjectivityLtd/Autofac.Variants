namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class ResourceKeyNotFoundException : Exception
    {
        private const string ResourceKeyNotFoundMessageFormat =
            "Couldn't find key '{0}'.";

        private const string ResourceKeyNotFoundForVariantIdMessageFormat =
            "Couldn't find key '{0}' in Embedded Resource named '{1}'.";

        public ResourceKeyNotFoundException()
        {
        }

        public ResourceKeyNotFoundException(string resourceKey)
            : base(GetMessage(resourceKey))
        {
        }

        public ResourceKeyNotFoundException(string resourceKey, string resourceName)
            : base(GetMessage(resourceKey, resourceName))
        {
        }

        public ResourceKeyNotFoundException(string resourceKey, Exception innerException)
            : base(GetMessage(resourceKey), innerException)
        {
        }

        public ResourceKeyNotFoundException(string resourceKey, string resourceName, Exception innerException)
            : base(GetMessage(resourceKey, resourceName), innerException)
        {
        }

        protected ResourceKeyNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string resourceKey, string resourceName = null)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return string.Format(CultureInfo.CurrentCulture, ResourceKeyNotFoundMessageFormat, resourceKey);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                ResourceKeyNotFoundForVariantIdMessageFormat,
                resourceKey,
                resourceName);
        }
    }
}
