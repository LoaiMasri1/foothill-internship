namespace AirportTicket.Common;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Ok() => new(true, Error.None);

    public static Result Failure(Error error)
    {
        return new(false, error);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue _value;
    protected internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess ? _value :
        throw new InvalidOperationException("The result does not contain a value.");

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public new static Result<TValue> Failure(Error error)
    {
        return new(default!, false, error);
    }
}
