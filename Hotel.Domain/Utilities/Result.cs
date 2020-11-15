namespace Hotel.Domain.Utilities
{
    public class Result
    {
        public bool IsError { get; set; }
        public string Message { get; set; }

        public bool IsSuccess => !IsError;

        public Result() { }

        public Result(bool isError, string message)
        {
            IsError = isError;
            Message = message;
        }

        public Result(bool isError)
        {
            IsError = isError;
            Message = null;
        }

        public static Result Ok(string message = "ok")
        {
            return new Result(false, message);
        }

        public static Result Fail(string message)
        {
            return new Result(true, message);
        }
    }

    public class Result<T>
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
        public bool IsSuccess
        {
            get
            {
                return !IsError;
            }
        }

        public Result(bool isError, string message, T value)
        {
            IsError = isError;
            Message = message;
            Value = value;
        }

        public Result(bool isError, T value)
        {
            IsError = isError;
            Value = value;
            Message = null;
        }

        public Result(bool isError, T value, string message)
        {
            IsError = isError;
            Value = value;
            Message = message;
        }

        public Result(bool isError, string message)
        {
            IsError = isError;
            Message = message;
            Value = default;
        }

        public Result(bool isError)
        {
            IsError = isError;
            Message = null;
            Value = default;
        }

        public Result() { }

        public static Result<T> Ok(T value, string message = "ok")
        {
            return new Result<T>(false, value, message);
        }

        public static Result<T> Ok(string message = "ok")
        {
            return new Result<T>(false, message);
        }

        public static Result<T> Fail(string message)
        {
            return new Result<T>(true, message);
        }

        public static Result<T> Fail(T value, string message)
        {
            return new Result<T>(true, value, message);
        }
    }
}
