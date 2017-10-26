namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        private const string ResourceNotFoundMessageFormat = "Couldn't find any Embedded Resources named '{0}'.";

        private const string ResourceNotFoundForVariantIdMessageFormat =
            "Couldn't find any Embedded Resources named '{0}' for variant id '{1}'.";

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

        public ResourceNotFoundException(string resourceName, string variantId, Exception innerException)
            : base(GetMessage(resourceName, variantId), innerException)
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
                return string.Format(CultureInfo.CurrentCulture, ResourceNotFoundMessageFormat, resourceName);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                ResourceNotFoundForVariantIdMessageFormat,
                resourceName,
                variantId);
        }
    }
}
