using System;
using System.Linq;
using Jinder.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Auth.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly JinderAuthContext _context;

        public AccountRepository(JinderAuthContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Account Get(String login, String password)
        {
            return _context.Accounts
                       .SingleOrDefault(a => a.Login == login && a.Password == password) ??
                   throw new ArgumentException();
        }

        public Account Get(Guid token)
        {
            return _context.Accounts
                       .Find(token) ?? throw new ArgumentException();
        }

        public Account Create(Account account)
        {
            account = _context.Accounts
                .Add(account)
                .Entity;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException();
            }

            return account;
        }

        public Boolean IsHaveWithLogin(String login)
        {
            return !(_context.Accounts
                       .SingleOrDefault(a => a.Login == login) is null);
        }
    }
}