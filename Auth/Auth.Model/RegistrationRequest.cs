
namespace Auth.Model
{
    public class RegistrationRequest
    {                   
        public string Email { get; set; }      
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string UserName { get; set; }      
        public string Password { get; set; }        
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }

        public RegistrationRequest(string email, string firstName, string lastName, string userName, string password, string confirmPassword, bool isAdmin)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
            IsAdmin = isAdmin;
        }
    }
}
