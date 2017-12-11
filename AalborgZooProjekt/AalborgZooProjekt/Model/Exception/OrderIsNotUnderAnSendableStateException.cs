using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class OrderIsNotUnderAnSendableStateException : Exception
    {
        public OrderIsNotUnderAnSendableStateException()
        {
        }

        public OrderIsNotUnderAnSendableStateException(string message) : base(message)
        {
        }

        public OrderIsNotUnderAnSendableStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderIsNotUnderAnSendableStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}