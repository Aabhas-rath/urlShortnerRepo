using System;
using System.Runtime.Serialization;

namespace CutURL.BusinessLayer
{
    [Serializable]
    public class ShorturlConflictException : Exception
    {
        public ShorturlConflictException()
        {
        }

        public ShorturlConflictException(string message) : base(message)
        {
        }

        public ShorturlConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShorturlConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}