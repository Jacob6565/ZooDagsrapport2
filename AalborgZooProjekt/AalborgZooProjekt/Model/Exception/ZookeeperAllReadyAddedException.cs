using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class ZookeeperAllReadyAddedException : Exception
    {
        public ZookeeperAllReadyAddedException()
        {
        }

        public ZookeeperAllReadyAddedException(string message) : base(message)
        {
        }

        public ZookeeperAllReadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ZookeeperAllReadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}