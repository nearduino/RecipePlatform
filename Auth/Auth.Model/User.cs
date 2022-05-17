using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Auth.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User(string fName, string lName, string uName, string mail, string pass)
        {
            this.Id = 4;
            this.FirstName = fName;
            this.LastName = lName;
            this.Username = uName;
            this.Email = mail;
            this.Password = pass;
        }
    }
}
