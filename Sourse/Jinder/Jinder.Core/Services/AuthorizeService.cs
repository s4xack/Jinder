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

        public AuthorizeService(IUserService userService, IAuthorizeClient authorizeClient)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            _authorizeClient = authorizeClient ?? throw new ArgumentNullException(nameof(authorizeClient));
        }

        public Guid Login(LoginDto credentials)
        {
            try
            {
                return _authorizeClient.Login(credentials).Result;
            }
            catch (AggregateException)
            {
                throw new ArgumentException();
            }
        }

        public Boolean Register(CreateAccountDto credentials)
        {
            if (!_authorizeClient.ValidateLogin(credentials.Login).Result)
                return true;

            Int32 userId = _userService.Create(credentials.User).Id;

            return _authorizeClient.Register(new RegisterDto()
                {Login = credentials.Login, Password = credentials.Password, UserId = userId}).Result;
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