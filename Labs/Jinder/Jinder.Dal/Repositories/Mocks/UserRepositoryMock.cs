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

        public UserRepositoryMock() :
            this(new List<User>
            {
                new User(
                    0,
                    "0@email.com",
                    "Admin",
                    string.Empty,
                    UserType.Administrator),
                new User(
                    1,
                    "1@email.com",
                    "Ivan",
                    string.Empty,
                    UserType.Candidate),
                new User(
                    2,
                    "2@email.com",
                    "Shepherd",
                    string.Empty,
                    UserType.Recruiter),
                new User(
                    3,
                    "3@email.com",
                    "Alex",
                    string.Empty,
                    UserType.Candidate),
                new User(
                    4,
                    "4@email.com",
                    "Max",
                    string.Empty,
                    UserType.Candidate),
                new User(
                    5,
                    "5@email.com",
                    "Roman",
                    string.Empty,
                    UserType.Recruiter),
                new User(
                    6,
                    "6@email.com",
                    "Nick",
                    string.Empty,
                    UserType.Recruiter)
            })
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