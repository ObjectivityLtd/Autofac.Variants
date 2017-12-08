namespace Autofac.Variants.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class ResourceKeyNotFoundException : Exception
    {
        private const string ResourceKeyNotFoundForVariantIdMessageFormat =
            "Couldn't find key '{0}' in Embedded Resource named '{1}'.";

        public ResourceKeyNotFoundException()
        {
        }

        public ResourceKeyNotFoundException(string resourceKey, string resourceName)
            : base(GetMessage(resourceKey, resourceName))
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

        private static string GetMessage(string resourceKey, string resourceName)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ResourceKeyNotFoundForVariantIdMessageFormat,
                resourceKey,
                resourceName);
        }
    }
}
