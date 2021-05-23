namespace Hotel.API.Wrappers
{
    public class SuccessResponse<T> : Response<T>
    {
        public SuccessResponse()
        { }

        public SuccessResponse(T data) : base(data)
        { }
    }
}
