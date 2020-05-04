using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.Repositories.Mocks
{
    public class UserRepositoryMock : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepositoryMock(List<User> users)
        {
            _users = users;
        }

        public UserRepositoryMock() : this(new List<User>())
        {
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User Get(Int32 userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId) ??
                   throw new ArgumentException($"No user with id {userId}!");
        }
    }
}