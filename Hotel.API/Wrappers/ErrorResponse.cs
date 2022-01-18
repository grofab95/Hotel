using System;
using System.Collections.Generic;

namespace Hotel.Api.Wrappers;

public class ErrorResponse : Response
{
    public ErrorResponse(Exception exception) : base(exception)
    { }

    public ErrorResponse(IEnumerable<string> errors) : base(errors)
    { }

    public ErrorResponse(string error) : base(error)
    { }
}