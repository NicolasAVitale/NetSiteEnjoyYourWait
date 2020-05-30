using System;
using System.Runtime.Serialization;

namespace EnjoyYourWaitNetSite.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException() : base()
        {
        }

        public DataAccessException(string message) : base(message)
        {
        }

        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}