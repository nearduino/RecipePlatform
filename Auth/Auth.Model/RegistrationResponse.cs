namespace Auth.Model
{
    public class RegistrationResponse
    {
       
        public string Message { get; set; }
        public string Status { get; set; }

        public string Token { get; set; }


        public RegistrationResponse(User user, string token)
        {
            Message = "User " + user.Username + " has been successfully created!";
           Token = token;
            Status = "200 OK.";
        }
    }
}
