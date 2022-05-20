using System.ComponentModel.DataAnnotations;

namespace Auth.Model
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public AuthenticateRequest(string usr, string pass)
        {
            Username = usr;
            Password = pass;
        }
    }
}