using Application.CQRs.Results;

public class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool isSuccess, T? data, string? error, string? errorCode)
        : base(isSuccess, error, errorCode)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
        => new(true, data, null, null);

    public static new Result<T> Failure(string error, string? errorCode = null)
        => new(false, default, error, errorCode);

    // Неявное приведение от T к Result<T>
    public static implicit operator Result<T>(T value)
        => Success(value);
}