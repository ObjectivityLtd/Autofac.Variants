namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class AmbigousResourceException : Exception
    {
        private const string AmbiguousResourceMessageFormat =
            "Error while resolving Embedded Resource named '{0}': more than one resource found.";

        private const string AmbiguousResourceForVariantIdMesssageFormat =
            "Error while resolving Embedded Resource named '{0}' for variant id = '{1}': more than one resource found.";

        public AmbigousResourceException()
        {
        }

        public AmbigousResourceException(string resourceName)
            : base(GetMessage(resourceName))
        {
        }

        public AmbigousResourceException(string resourceName, string variantId)
            : base(GetMessage(resourceName, variantId))
        {
        }

        public AmbigousResourceException(string resourceName, Exception innerException)
            : base(GetMessage(resourceName), innerException)
        {
        }

        public AmbigousResourceException(string resourceName, string variantId, Exception innerException)
            : base(GetMessage(resourceName, variantId), innerException)
        {
        }

        protected AmbigousResourceException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string resourceName, string variantId = null)
        {
            if (string.IsNullOrEmpty(variantId))
            {
                return string.Format(CultureInfo.CurrentCulture, AmbiguousResourceMessageFormat, resourceName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                AmbiguousResourceForVariantIdMesssageFormat,
                resourceName,
                variantId);
        }
    }
}
