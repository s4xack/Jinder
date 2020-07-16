using System;
using Jinder.Core.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Core.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAuthorizeClient _authorizeClient;
        private readonly IUserService _userService;

        public AuthorizeService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            _authorizeClient = RestService.For<IAuthorizeClient>("http://localhost:64642");
        }

        public Guid Login(String login, String password)
        {
            try
            {
                return _authorizeClient.Login(login, password).Result;
            }
            catch (AggregateException)
            {
                throw new ArgumentException();
            }
        }

        public Boolean Register(String login, String password, CreateUserDto user)
        {
            if (!_authorizeClient.ValidateLogin(login).Result)
                return true;

            Int32 userId = _userService.Create(user).Id;

            return _authorizeClient.Register(login, password, userId).Result;
        }

        public Boolean ValidateLogin(String login)
        {
            return _authorizeClient.ValidateLogin(login).Result;
        }

        public Int32 ValidateToken(Guid token)
        {
            return _authorizeClient.ValidateToken(token).Result;
        }
    }
}