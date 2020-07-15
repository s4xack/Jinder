using System;
using System.Configuration;
using Jinder.Desktop.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserClient _userClient;

        public UserService()
        {
            _userClient = RestService.For<IUserClient>(Session.HostUrl);
        }

        public UserDto GetMe(Guid token)
        {
            return _userClient.GetMe(token).Result;
        }
    }
}