using System;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class User
    {
        public Int32 Id { get; }
        public String Email { get; }
        public String Name { get; }
        public String PasswordHash { get; }
        public UserType Type { get; }

        public User(Int32 id, String email, String name, String passwordHash, UserType type)
        {
            Id = id;
            Email = email;
            Name = name;
            PasswordHash = passwordHash;
            Type = type;
        }
    }
}