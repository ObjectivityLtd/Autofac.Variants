namespace Autofac.Variants.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class DefaultVariantInterfaceNotFoundException : Exception
    {
        private const string DefaultVariantNotFoundMessageFormat = "Couldn't find default type for variant interface '{0}'.";

        public DefaultVariantInterfaceNotFoundException()
        {
        }

        public DefaultVariantInterfaceNotFoundException(string variantInterface)
            : base(GetMessage(variantInterface))
        {
        }

        public DefaultVariantInterfaceNotFoundException(string variantInterface, Exception innerException)
            : base(GetMessage(variantInterface), innerException)
        {
        }

        protected DefaultVariantInterfaceNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string variantInterface)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                DefaultVariantNotFoundMessageFormat,
                variantInterface);
        }
    }
}
