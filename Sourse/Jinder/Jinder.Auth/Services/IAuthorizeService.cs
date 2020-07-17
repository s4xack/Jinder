using System;

namespace Jinder.Auth.Services
{
    public interface IAuthorizeService
    {
        Guid Login(string login, string password);
        Boolean Register(string login, string password, Int32 userId);
        Boolean ValidateLogin(string login);
        Int32 ValidateToken(Guid token);
    }
}