using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.API.Wrappers
{
    public class Response<T>
    {
        public T Data { get; private set; }
        public bool Succeeded { get; private set; }
        public string[] Errors { get; private set; }

        public Response(T data)
        {
            Data = data;
            Succeeded = true;
        }
    }

    public class Response
    {
        public bool Succeeded { get; private set; }
        public string[] Errors { get; private set; }

        public Response(Exception exception)
        {
            Errors = new string[] { exception.Message };
            Succeeded = false;
        }

        public Response(IEnumerable<string> errors)
        {
            Errors = errors.ToArray();
            Succeeded = false;
        }
    }
}
