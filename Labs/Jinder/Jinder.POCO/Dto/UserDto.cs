using System;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class UserDto
    {
        public Int32 Id { get; }
        public String Name { get; }
        public String Email { get; }
        public UserType Type { get; }

        [JsonConstructor]
        private UserDto(Int32 id, String name, String email, UserType type)
        {
            Id = id;
            Name = name;
            Email = email;
            Type = type;
        }

        public static UserDto Create(User user)
        {
            return new UserDto(user.Id, user.Name, user.Email, user.Type);
        }
    }
}