using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Auth.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }

        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; private set; }
        public bool IsAdmin { get; private set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User(string fName, string lName, string uName, string mail, string pass, bool isAdmin)
        {     
            FirstName = fName;
            LastName = lName;
            UserName = uName;
            Email = mail;
            Password = pass;
            IsAdmin = isAdmin;
        }
        public User(string fName, string lName, string uName, string mail, string pass, bool isAdmin, int id)
        {
            Id = id;
            FirstName = fName;
            LastName = lName;
            UserName = uName;
            Email = mail;
            Password = pass;
            IsAdmin = isAdmin;
        }
    }
}
