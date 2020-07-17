using System;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services
{
    public interface IUserService
    {
        UserDto GetMe(Guid token);
    }
}