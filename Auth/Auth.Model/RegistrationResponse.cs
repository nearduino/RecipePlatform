namespace Auth.Model
{
    public class RegistrationResponse
    {
       
        public string Message { get; set; }
        public bool Status { get; set; }

        public string Token { get; set; }

        public RegistrationResponse(string message)
        {
            Message = message;
            Status = false;
        }
        public RegistrationResponse(User user, string token)
        {
            
            Message = "User " + user.Username + " has been successfully created!";
           Token = token;
            Status = true;
        }
    }
}
