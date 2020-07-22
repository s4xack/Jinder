using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.DbEntities;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Microsoft.EntityFrameworkCore;

namespace Jinder.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JinderContext _context;

        public UserRepository(JinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyCollection<User> GetAll()
        {
            return _context.Users
                .Select(u => u.ToModel())
                .ToList();
        }

        public User Get(Int32 userId)
        {
            return _context.Users
                .Find(userId)
                ?.ToModel() ?? throw new ArgumentException($"No user with id {userId}!");
        }

        public User Add(User user)
        {
            DbUser dbUser = DbUser.FromModel(user);

            dbUser = _context.Users
                .Add(dbUser)
                .Entity;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Unable to create user with such data!");
            }

            return dbUser.ToModel();
        }
    }
}