using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Auth.Model
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public bool IsAdmin { get; private set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string Confirm { get; set; }

        [JsonIgnore]
        public string Salt { get; set; }
        public User(string fName, string lName, string uName, string mail, string pass, bool isAdmin, string salt)
        {     
            FirstName = fName;
            LastName = lName;
            UserName = uName;
            Email = mail;
            Password = pass;
            IsAdmin = isAdmin;
            Salt = salt;
        }
        public User(string fName, string lName, string uName, string mail, string pass, bool isAdmin, Guid id, string salt)
        {
            Id = id;
            FirstName = fName;
            LastName = lName;
            UserName = uName;
            Email = mail;
            Password = pass;
            IsAdmin = isAdmin;
            Salt = salt;
        }
       
    }
}
