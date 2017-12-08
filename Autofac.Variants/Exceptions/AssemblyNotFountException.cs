namespace Autofac.Variants.Exceptions
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class AssemblyNotFountException : Exception
    {
        private const string AssemblyNotFoundMessageFormat = "Couldn't find assembly named {0}.";

        public AssemblyNotFountException()
        {
        }

        public AssemblyNotFountException(string assemblyName)
            : base(GetMessage(assemblyName))
        {
        }

        public AssemblyNotFountException(string assemblyName, Exception innerException)
            : base(GetMessage(assemblyName), innerException)
        {
        }

        protected AssemblyNotFountException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        private static string GetMessage(string assemblyName)
        {
            return string.Format(CultureInfo.CurrentCulture, AssemblyNotFoundMessageFormat, assemblyName);
        }
    }
}
