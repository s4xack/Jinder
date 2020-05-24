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
                UserType.Candidate => Candidate.Token,
                UserType.Recruiter => Recruiter.Token,
                _ => throw new ArgumentException()
            };
        }
    }
}