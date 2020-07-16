using System;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface IAuthorizeService
    {
        Guid Login(string login, string password);

        Boolean Register(string login, string password, CreateUserDto user);

        Boolean ValidateLogin(string login);

        Int32 ValidateToken(Guid token);
    }
}