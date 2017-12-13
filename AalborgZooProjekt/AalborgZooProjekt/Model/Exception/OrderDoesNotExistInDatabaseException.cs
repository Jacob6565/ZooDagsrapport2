using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    public class OrderDoesNotExistInDatabaseException : Exception
    {
        public OrderDoesNotExistInDatabaseException()
        {
        }

        public OrderDoesNotExistInDatabaseException(string message) : base(message)
        {
        }

        public OrderDoesNotExistInDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderDoesNotExistInDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}