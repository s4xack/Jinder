using System;
using Jinder.Poco.Types;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class CreateUserDto
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public UserType Type { get; set; }

        public CreateUserDto()
        {
        }

        [JsonConstructor]
        public CreateUserDto(String name, String email, UserType type)
        {
            Name = name;
            Email = email;
            Type = type;
        }
    }
}