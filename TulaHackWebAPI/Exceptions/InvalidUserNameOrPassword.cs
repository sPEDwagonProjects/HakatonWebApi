namespace TulaHackWebAPI.Exceptions
{
    public class InvalidUserNameOrPassword:ExceptionBase
    {
        public InvalidUserNameOrPassword()
        {
            errorCode = 1;
            message = "Invalid username or password";
        }
    }
}
