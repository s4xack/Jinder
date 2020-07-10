using System;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Email { get; private set; }
        public String Name { get; private set; }
        public String PasswordHash { get; set; }
        public UserType Type { get; private set; }

        public User()
        {
        }

        public User(String email, String name, String passwordHash, UserType type)
        {
            Email = email;
            Name = name;
            PasswordHash = passwordHash;
            Type = type;
        }
    }
}