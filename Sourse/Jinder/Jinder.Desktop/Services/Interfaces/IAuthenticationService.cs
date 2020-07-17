using System;
using Jinder.Poco.Types;

namespace Jinder.Desktop.Services
{
    public interface IAuthenticationService
    {
        Guid Authenticate(UserType userType);
    }
}