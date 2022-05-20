
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

        public RegistrationRequest(string email, string fname, string lname, string usrname, string pass, string confirm, bool admin)
        {
            Email = email;
            FirstName = fname;
            LastName = lname;
            UserName = usrname;
            Password = pass;
            ConfirmPassword = confirm;
            IsAdmin = admin;
        }
    }
}
