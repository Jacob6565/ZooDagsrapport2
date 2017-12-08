using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class ProductAlreadyHaveThatNameException : Exception
    {
        public ProductAlreadyHaveThatNameException()
        {
        }

        public ProductAlreadyHaveThatNameException(string message) : base(message)
        {
        }

        public ProductAlreadyHaveThatNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductAlreadyHaveThatNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}