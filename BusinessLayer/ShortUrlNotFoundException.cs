using System;
using System.Runtime.Serialization;

namespace CutURL.BusinessLayer
{
    [Serializable]
    public class ShortUrlNotFoundException : Exception
    {
        public ShortUrlNotFoundException()
        {
        }

        public ShortUrlNotFoundException(string message) : base(message)
        {
        }

        public ShortUrlNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShortUrlNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}