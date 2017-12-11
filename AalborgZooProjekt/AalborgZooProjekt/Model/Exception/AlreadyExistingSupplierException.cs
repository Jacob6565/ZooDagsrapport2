using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class AlreadyExistingSupplierException : Exception
    {
        public AlreadyExistingSupplierException()
        {
        }

        public AlreadyExistingSupplierException(string message) : base(message)
        {
        }

        public AlreadyExistingSupplierException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistingSupplierException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}