using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface IUserClient
    {
        [Get("/api/user/get/me")]
        Task<UserDto> GetMe([Header("token")] Guid token);
    }
}