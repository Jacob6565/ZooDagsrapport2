using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    public class OrderCanNotSendNoZookeeperException : Exception
    {
        public OrderCanNotSendNoZookeeperException()
        {
        }

        public OrderCanNotSendNoZookeeperException(string message) : base(message)
        {
        }

        public OrderCanNotSendNoZookeeperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderCanNotSendNoZookeeperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}