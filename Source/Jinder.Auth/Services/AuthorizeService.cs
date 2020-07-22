using System;
using Jinder.Auth.Models;
using Jinder.Auth.Repositories;
using Jinder.Poco.Dto;

namespace Jinder.Auth.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthorizeService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public Guid Login(LoginDto credentials)
        {
            return _accountRepository.Get(credentials.Login, credentials.Password).Token;
        }

        public Boolean Register(RegisterDto credentials)
        {
            if (!ValidateLogin(credentials.Login))
                return false;

            try
            {
                _accountRepository.Create(new Account()
                    {Login = credentials.Login, Password = credentials.Password, UserId = credentials.UserId});
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public Boolean ValidateLogin(String login)
        {
            return !_accountRepository.IsHaveWithLogin(login);
        }

        public Int32 ValidateToken(Guid token)
        {
            return _accountRepository.Get(token).UserId;
        }
    }
}