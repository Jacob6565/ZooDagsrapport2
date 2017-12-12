using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class ProductVersionDoesNotContainGivenUnitException : Exception
    {
        public ProductVersionDoesNotContainGivenUnitException()
        {
        }

        public ProductVersionDoesNotContainGivenUnitException(string message) : base(message)
        {
        }

        public ProductVersionDoesNotContainGivenUnitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductVersionDoesNotContainGivenUnitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}