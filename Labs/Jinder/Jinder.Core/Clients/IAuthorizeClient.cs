using System;
using System.Threading.Tasks;
using Refit;

namespace Jinder.Core.Clients
{
    public interface IAuthorizeClient
    {
        [Get("/authorize/login/{login}/{password}")]
        Task<Guid> Login(string login, string password);

        [Get("/authorize/register/{userId}/{login}/{password}")]
        Task<Boolean> Register(string login, string password, Int32 userId);

        [Get("/authorize/validate/login/{login}")]
        Task<Boolean> ValidateLogin(string login);

        [Get("/authorize/validate/token/{token}")]
        Task<Int32> ValidateToken(Guid token);
    }
}