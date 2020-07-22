using System;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface IAuthorizeService
    {
        Guid Login(LoginDto credentials);

        Boolean Register(CreateAccountDto credentials);

        Boolean ValidateLogin(string login);

        Int32 ValidateToken(Guid token);
    }
}