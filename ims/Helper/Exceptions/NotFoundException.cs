namespace ims.Helper.Exceptions;
public class NotFoundException : ArgumentNullException
{
    public NotFoundException(string message) : base(message) { }
}

