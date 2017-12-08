using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class ProductVersionIsNotActiveException : Exception
    {
        public ProductVersionIsNotActiveException()
        {
        }

        public ProductVersionIsNotActiveException(string message) : base(message)
        {
        }

        public ProductVersionIsNotActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductVersionIsNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}