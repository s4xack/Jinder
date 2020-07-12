using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.Repositories.Mocks
{
    public class UserRepositoryMock : IUserRepository
    {
        private readonly List<User> _users;
        private readonly Int32 _newId;

        public UserRepositoryMock(List<User> users)
        {
            _newId = 0;
            foreach (var user in users)
            {
                user.Id = _newId++;
            }
            _users = users;
        }

        public UserRepositoryMock() : this(new List<User>())
        {
        }

        public IReadOnlyCollection<User> GetAll()
        {
            return _users;
        }

        public User Get(Int32 userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId) ??
                   throw new ArgumentException($"No user with id {userId}!");
        }

        public User Add(User user)
        {
            throw new NotImplementedException();
        }
    }
}