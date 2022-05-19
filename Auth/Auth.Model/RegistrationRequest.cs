using Auth.Model.Validators;
using System.ComponentModel.DataAnnotations;


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
    }
}
