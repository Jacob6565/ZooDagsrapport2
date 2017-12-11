using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    internal class DepartmentAlreadyDepartmentSpecificProductException : Exception
    {
        public DepartmentAlreadyDepartmentSpecificProductException()
        {
        }

        public DepartmentAlreadyDepartmentSpecificProductException(string message) : base(message)
        {
        }

        public DepartmentAlreadyDepartmentSpecificProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DepartmentAlreadyDepartmentSpecificProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
