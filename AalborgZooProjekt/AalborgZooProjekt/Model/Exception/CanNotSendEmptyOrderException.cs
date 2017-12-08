using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class CanNotSendEmptyOrderException : Exception
    {
        public CanNotSendEmptyOrderException()
        {
        }

        public CanNotSendEmptyOrderException(string message) : base(message)
        {
        }

        public CanNotSendEmptyOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CanNotSendEmptyOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}