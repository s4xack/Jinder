using System;
using Jinder.Auth.Models;

namespace Jinder.Auth.Repositories
{
    public interface IAccountRepository
    {
        Account Get(string login, string password);
        Account Get(Guid token);
        Account Create(Account account);
        Boolean IsHaveWithLogin(string login);
    }
}