namespace TulaHackWebAPI.Exceptions
{
    public class UserExist : ExceptionBase
    {
        public UserExist()
        {
            errorCode = 1;
            message = "Пользователь уже зарегестрирован";
                
        }
    }
}
