using AirportTicket.Common;

namespace AirportTikcet.Test.Helpers;

public static class ResultTestHelper
{
    public static Result<TValue> CreateSuccess<TValue>(TValue value)
        => Result<TValue>.Success(value);


    public static Result<TValue> CreateFailure<TValue>(Error error)
        => Result<TValue>.Failure(error);


    public static Result CreateSuccess()
        => Result.Ok();


    public static Result CreateFailure(Error error)
        => Result.Failure(error);

}