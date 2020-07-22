using System;
using Jinder.Poco.Dto;

namespace Jinder.Auth.Services
{
    public interface IAuthorizeService
    {
        Guid Login(LoginDto credentials);
        Boolean Register(RegisterDto credentials);
        Boolean ValidateLogin(string login);
        Int32 ValidateToken(Guid token);
    }
}