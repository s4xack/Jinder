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
                new User
                {
                    Email = "0@email.com",
                    Id = 0,
                    Name = "Admin",
                    PasswordHash = string.Empty,
                    Type = UserType.Administrator
                },
                new User
                {
                    Email = "1@email.com",
                    Id = 1,
                    Name = "Ivan",
                    PasswordHash = string.Empty,
                    Type = UserType.Candidate
                },
                new User
                {
                    Email = "2@email.com",
                    Id = 2,
                    Name = "Shepherd",
                    PasswordHash = string.Empty,
                    Type = UserType.Recruiter
                },
                new User
                {
                    Email = "3@email.com",
                    Id = 3,
                    Name = "Alex",
                    PasswordHash = string.Empty,
                    Type = UserType.Candidate
                },
                new User
                {
                    Email = "4@email.com",
                    Id = 4,
                    Name = "Max",
                    PasswordHash = string.Empty,
                    Type = UserType.Candidate
                },
                new User
                {
                    Email = "5@email.com",
                    Id = 5,
                    Name = "Roman",
                    PasswordHash = string.Empty,
                    Type = UserType.Recruiter
                },
                new User
                {
                    Email = "6@email.com",
                    Id = 6,
                    Name = "Nick",
                    PasswordHash = string.Empty,
                    Type = UserType.Recruiter
                }
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