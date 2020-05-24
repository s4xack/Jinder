using System;
using Jinder.Desktop.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services.Mock
{
    public class UserServiceMock : IUserService
    {
        public UserDto GetMe(Guid token)
        {
            if (token.CompareTo(Candidate.Token) == 0)
                return Candidate.User;
            else if (token.CompareTo(Recruiter.Token) == 0)
                return Recruiter.User;
            else
                throw new ArgumentException();
        }
    }
}