namespace Autofac.Variants.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        private const string ResourceNotFoundForDefaultVariantMessageFormat =
            "Couldn't find any embedded resources named '{0}' for default variant.";

        private const string ResourceNotFoundForVariantIdMessageFormat =
            "Couldn't find any embedded resources named '{0}' for VariantId '{1}'.";

        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string resourceName)
            : base(GetMessage(resourceName))
        {
        }

        public ResourceNotFoundException(string resourceName, string variantId)
            : base(GetMessage(resourceName, variantId))
        {
        }

        public ResourceNotFoundException(string resourceName, Exception innerException)
            : base(GetMessage(resourceName), innerException)
        {
        }

        protected ResourceNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string resourceName, string variantId = null)
        {
            if (string.IsNullOrEmpty(variantId))
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    ResourceNotFoundForDefaultVariantMessageFormat,
                    resourceName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                ResourceNotFoundForVariantIdMessageFormat,
                resourceName,
                variantId);
        }
    }
}
