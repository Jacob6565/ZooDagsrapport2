using System;
using System.Runtime.Serialization;

namespace AalborgZooProjekt.Model
{
    [Serializable]
    public class DepartmentNotDepartmentSpecificProductException : Exception
    {
        public DepartmentNotDepartmentSpecificProductException()
        {
        }

        public DepartmentNotDepartmentSpecificProductException(string message) : base(message)
        {
        }

        public DepartmentNotDepartmentSpecificProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DepartmentNotDepartmentSpecificProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}