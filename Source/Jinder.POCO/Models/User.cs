using System;
using Jinder.Poco.Types;

namespace Jinder.Poco.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Email { get; }
        public String Name { get; }
        public UserType Type { get; }

        public User(String email, String name, UserType type)
        {
            Email = email;
            Name = name;
            Type = type;
        }
    }
}