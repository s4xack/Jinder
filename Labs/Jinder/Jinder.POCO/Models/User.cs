using System;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Email { get; }
        public String Name { get; }
        public String PasswordHash { get; }
        public UserType Type { get; }

        public User(String email, String name, String passwordHash, UserType type)
        {
            Email = email;
            Name = name;
            PasswordHash = passwordHash;
            Type = type;
        }
    }
}