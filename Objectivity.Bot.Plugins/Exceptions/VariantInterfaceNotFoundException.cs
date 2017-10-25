namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class VariantInterfaceNotFoundException : Exception
    {
        private const string VariantNotFoundMessageFormat = "Couldn't find any variants of type '{0}'.";

        private const string VariantNotFoundForTenantMessageFormat = "Couldn't find any variants of type '{0}' for variant id '{1}'.";

        public VariantInterfaceNotFoundException()
        {
        }

        public VariantInterfaceNotFoundException(string variantInterface)
            : base(GetMessage(variantInterface))
        {
        }

        public VariantInterfaceNotFoundException(string variantInterface, string variantId)
            : base(GetMessage(variantInterface, variantId))
        {
        }

        public VariantInterfaceNotFoundException(string variantInterface, Exception innerException)
            : base(GetMessage(variantInterface), innerException)
        {
        }

        public VariantInterfaceNotFoundException(string variantInterface, string variantId, Exception innerException)
            : base(GetMessage(variantInterface, variantId), innerException)
        {
        }

        protected VariantInterfaceNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string variantInterface, string variantId = null)
        {
            if (string.IsNullOrEmpty(variantId))
            {
                return string.Format(CultureInfo.CurrentCulture, VariantNotFoundMessageFormat, variantInterface);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                VariantNotFoundForTenantMessageFormat,
                variantInterface,
                variantId);
        }
    }
}
