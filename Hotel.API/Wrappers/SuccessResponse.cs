namespace Hotel.Api.Wrappers;

public class SuccessResponse<T> : Response<T>
{
    public SuccessResponse()
    { }

    public SuccessResponse(T data) : base(data)
    { }
}

public class SuccessResponse : Response
{
    public SuccessResponse() : base()
    { }
}