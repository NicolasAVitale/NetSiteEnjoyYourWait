using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EnjoyYourWaitNetSite.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException() : base()
        {
        }

        public AuthException(string message) : base(message)
        {
        }

        public AuthException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AuthException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}