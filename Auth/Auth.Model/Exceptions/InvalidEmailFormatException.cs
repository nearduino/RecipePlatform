using System;


namespace Auth.Model.Exceptions
{
    public class InvalidEmailFormatException : ArgumentException
    {
        public InvalidEmailFormatException() : base("Email format is invalid!")
        {

        }
    }
}
