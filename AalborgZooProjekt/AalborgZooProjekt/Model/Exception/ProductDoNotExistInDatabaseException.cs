using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class ProductDoNotExistInDatabaseException : Exception
    {
        public ProductDoNotExistInDatabaseException()
        {
        }

        public ProductDoNotExistInDatabaseException(string message) : base(message)
        {
        }

        public ProductDoNotExistInDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductDoNotExistInDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}