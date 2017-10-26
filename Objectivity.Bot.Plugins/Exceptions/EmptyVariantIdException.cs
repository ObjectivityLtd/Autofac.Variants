namespace Objectivity.Bot.Plugins.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class EmptyVariantIdException : Exception
    {
        private const string EmptyVariantIdMessage =
            "Couldn't register variants module for empty VariantId provided in settings.";

        public EmptyVariantIdException()
            : base(EmptyVariantIdMessage)
        {
        }

        public EmptyVariantIdException(string message)
            : base(GetMessage(message))
        {
        }

        public EmptyVariantIdException(string message, Exception innerException)
            : base(GetMessage(message), innerException)
        {
        }

        protected EmptyVariantIdException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string customMessage)
        {
            var pattern = "{0}: {1}";
            return string.Format(CultureInfo.InvariantCulture, pattern, EmptyVariantIdMessage, customMessage);
        }
    }
}
