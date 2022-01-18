using System;

namespace Hotel.Domain.Exceptions;

public class MissingValueException : Exception
{
    public MissingValueException(string message) : base(message)
    { }
}