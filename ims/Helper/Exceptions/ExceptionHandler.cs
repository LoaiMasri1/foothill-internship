namespace ims.Helper.Exceptions;
public class ExceptionHandler
{
    public static void Handle(Action func)
    {
        try
        {
            func.Invoke();
        }
        catch (AlreadyExistException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
        catch (NotFoundException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }


        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message, ex.StackTrace);
        }

    }

}
