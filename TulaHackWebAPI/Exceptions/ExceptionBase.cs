namespace TulaHackWebAPI.Exceptions
{
    public class ExceptionBase
    {
        //Код ошибки
        public int errorCode { get; set; }

        //Имя ошибки
        public string? message { get; set; }
    }
}
