using System;
using System.Runtime.Serialization;

namespace Hotel.Application.Exceptions
{
    public class HotelException : Exception
    {
        public HotelException(string message) : base(message)
        { }

        public HotelException(string message, Exception innerException) : base(message, innerException)
        { }

        protected HotelException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
