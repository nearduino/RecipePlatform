using System;


namespace Auth.Model.Exceptions
{
   public class LogInException : ArgumentException
    {
        public LogInException() : base("Wrong username or password!")
        {

        }
    }
}
