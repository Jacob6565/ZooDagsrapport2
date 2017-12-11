using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    public class UnitAlreadyRemovedException : Exception
    {
        public UnitAlreadyRemovedException()
        {
        }

        public UnitAlreadyRemovedException(string message) : base(message)
        {
        }

        public UnitAlreadyRemovedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnitAlreadyRemovedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}