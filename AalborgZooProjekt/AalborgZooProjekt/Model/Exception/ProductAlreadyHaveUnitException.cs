using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    public class ProductAlreadyHaveUnitException : Exception
    {
        public ProductAlreadyHaveUnitException()
        {
        }

        public ProductAlreadyHaveUnitException(string message) : base(message)
        {
        }

        public ProductAlreadyHaveUnitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductAlreadyHaveUnitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}