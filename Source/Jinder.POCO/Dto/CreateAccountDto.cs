using System;

namespace Jinder.Poco.Dto
{
    public class CreateAccountDto
    {
        public String Login { get; set; }
        public String Password { get; set; }

        public CreateUserDto User { get; set; }
    }
}