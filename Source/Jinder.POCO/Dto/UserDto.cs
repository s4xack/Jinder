using System;
using Newtonsoft.Json;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Poco.Dto
{
    public class UserDto
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public UserType Type { get; set; }

        [JsonConstructor]
        public UserDto(Int32 id, String name, String email, UserType type)
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