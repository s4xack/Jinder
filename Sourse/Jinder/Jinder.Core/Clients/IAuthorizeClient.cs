using System;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Core.Clients
{
    public interface IAuthorizeClient
    {
        [Post("/authorize/login")]
        Task<Guid> Login([Body]LoginDto credentials);

        [Post("/authorize/register")]
        Task<Boolean> Register([Body]RegisterDto credentials);

        [Get("/authorize/validate/login/{login}")]
        Task<Boolean> ValidateLogin(string login);

        [Get("/authorize/validate/token/{token}")]
        Task<Int32> ValidateToken(Guid token);
    }
}