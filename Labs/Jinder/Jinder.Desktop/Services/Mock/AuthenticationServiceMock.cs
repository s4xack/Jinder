using System;
using Jinder.Desktop.Mock;
using Jinder.Poco.Types;

namespace Jinder.Desktop.Services.Mock
{
    public class AuthenticationServiceMock : IAuthenticationService
    {
        public Guid Authenticate(UserType userType)
        {
            return userType switch
            {
                UserType.Candidate => Guid.Parse("2C9D9DF4-F80A-4A09-FF8F-08D82925FFFC"),
                UserType.Recruiter => Guid.Parse("52968EB9-891A-41E6-FF90-08D82925FFFC"),
                _ => throw new ArgumentException()
            };
        }
    }
}