using System;

namespace Hotel.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message) : base(message)
    { }
}