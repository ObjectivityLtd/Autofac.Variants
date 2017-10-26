namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class EmptyVariantIdException : Exception
    {
        private const string EmptyVariantIdMessage = "Couldn't register variants module for empty Variant Id.";

        public EmptyVariantIdException()
            : base(EmptyVariantIdMessage)
        {
        }

        public EmptyVariantIdException(string message)
            : base(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", EmptyVariantIdMessage, message))
        {
        }

        public EmptyVariantIdException(string message, Exception innerException)
            : base(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", EmptyVariantIdMessage, message), innerException)
        {
        }

        protected EmptyVariantIdException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
