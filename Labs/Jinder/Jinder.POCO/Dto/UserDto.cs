using Jinder.Poco.Type;

namespace Jinder.Poco.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}
