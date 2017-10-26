namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class AmbigousVariantInterfaceException : Exception
    {
        private const string AmbiguousVariantInterfacesForDefaultVariantMessageFormat =
            "Error while resolving variant interface '{0}' for default variant: more than one type found.";

        private const string AmbiguousVariantInterfacesForVariantIdMesssageFormat =
            "Error while resolving variant interface '{0}' for VariantId '{1}': more than one type found.";

        public AmbigousVariantInterfaceException()
        {
        }

        public AmbigousVariantInterfaceException(string variantInterface)
            : base(GetMessage(variantInterface))
        {
        }

        public AmbigousVariantInterfaceException(string variantInterface, string variantInterfaceId)
            : base(GetMessage(variantInterface, variantInterfaceId))
        {
        }

        public AmbigousVariantInterfaceException(string variantInterface, string variantInterfaceId, Exception innerException)
            : base(GetMessage(variantInterface, variantInterfaceId), innerException)
        {
        }

        protected AmbigousVariantInterfaceException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string variantInterface, string variantInterfaceId = null)
        {
            if (string.IsNullOrEmpty(variantInterfaceId))
            {
                return string.Format(CultureInfo.CurrentCulture, AmbiguousVariantInterfacesForDefaultVariantMessageFormat, variantInterface);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                AmbiguousVariantInterfacesForVariantIdMesssageFormat,
                variantInterface,
                variantInterfaceId);
        }
    }
}
