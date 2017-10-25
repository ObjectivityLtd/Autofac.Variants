namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class DefaultVariantInterfaceNotFoundException : Exception
    {
        private const string DefaultVariantNotFoundMessageFormat = "Couldn't find default variantInterface of type '{0}'.";

        public DefaultVariantInterfaceNotFoundException()
        {
        }

        public DefaultVariantInterfaceNotFoundException(string variant)
            : base(GetMessage(variant))
        {
        }

        public DefaultVariantInterfaceNotFoundException(string variant, Exception innerException)
            : base(GetMessage(variant), innerException)
        {
        }

        protected DefaultVariantInterfaceNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string variantInterface, string variantId = null)
        {
            if (string.IsNullOrEmpty(variantId))
            {
                return string.Format(CultureInfo.CurrentCulture, DefaultVariantNotFoundMessageFormat, variantInterface);
            }

            return string.Format(
                CultureInfo.CurrentCulture,
                DefaultVariantNotFoundMessageFormat,
                variantInterface);
        }
    }
}
