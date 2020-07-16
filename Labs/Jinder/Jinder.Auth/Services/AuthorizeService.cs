using System;
using Jinder.Auth.Models;
using Jinder.Auth.Repositories;

namespace Jinder.Auth.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthorizeService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public Guid Login(String login, String password)
        {
            return _accountRepository.Get(login, password).Token;
        }

        public Boolean Register(String login, String password, Int32 userId)
        {
            if (!ValidateLogin(login))
                return false;

            try
            {
                _accountRepository.Create(new Account() {Login = login, Password = password, UserId = userId});
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