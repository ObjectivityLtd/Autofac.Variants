namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class AmbigousResourceException : Exception
    {
        private const string AmbiguousResourceForDefaultVariantMessageFormat =
            "Error while resolving embedded resource named '{0}' for default variant: more than one resource found.";

        private const string AmbiguousResourceForVariantIdMesssageFormat =
            "Error while resolving embedded resource named '{0}' for VariantId '{1}': more than one resource found.";

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
                return string.Format(
                    CultureInfo.CurrentCulture,
                    AmbiguousResourceForDefaultVariantMessageFormat,
                    resourceName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                AmbiguousResourceForVariantIdMesssageFormat,
                resourceName,
                variantId);
        }
    }
}
