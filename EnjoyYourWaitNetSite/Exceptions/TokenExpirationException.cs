using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnjoyYourWaitNetSite.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException() : base()
        {
        }

        public TokenExpiredException(string message) : base(message)
        {
        }

        public TokenExpiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TokenExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}