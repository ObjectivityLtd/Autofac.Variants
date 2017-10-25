namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class AmbigousVariantInterfaceException : Exception
    {
        private const string AmbiguousvariantInterfacesMessageFormat =
            "Error while resolving variantInterface type '{0}': more than one type found.";

        private const string AmbiguousvariantInterfacesForvariantInterfaceIdMesssageFormat =
            "Error while resolving variantInterface type '{0}' for variantInterface id '{1}': more than one type found.";

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

        public AmbigousVariantInterfaceException(string variantInterface, Exception innerException)
            : base(GetMessage(variantInterface), innerException)
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
                return string.Format(CultureInfo.CurrentCulture, AmbiguousvariantInterfacesMessageFormat, variantInterface);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                AmbiguousvariantInterfacesForvariantInterfaceIdMesssageFormat,
                variantInterface,
                variantInterfaceId);
        }
    }
}
