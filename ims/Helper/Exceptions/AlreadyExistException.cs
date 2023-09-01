namespace ims.Helper.Exceptions;
public class AlreadyExistException : ArgumentException
{
    public AlreadyExistException(string message) : base(message) { }
}
