using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Core.Services;
using Jinder.Dal.Repositories;
using Jinder.Dal.Repositories.Mocks;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using NUnit.Framework;

namespace Jinder.Test.Services
{
    public static class UserDtoExtension
    {
        public static Boolean EquivalentTo(this UserDto user, UserDto other)
        {
            return user.Id == other.Id &&
                   user.Name == other.Name &&
                   user.Email == other.Email &&
                   user.Type == other.Type;
        }
    }

    [TestFixture]
    public class UserServiceShould
    {
        [SetUp]
        public void UserServiceSetUp()
        {
            var users = new List<User>
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
            };

            _userRepository = new UserRepositoryMock(users);
            _userService = new UserService(_userRepository);
        }

        private IUserService _userService;
        private IUserRepository _userRepository;

        [Test]
        public void Should_return_all_users_same_as_from_repository()
        {
            // Arrange
            var expected = _userRepository
                .GetAll()
                .Select(UserDto.Create)
                .ToList();

            // Act
            var result = _userService.GetAll();

            // Assert
            Assert.That(result != null);
            Assert.That(result.Count == expected.Count);
            for (var i = 0; i < result.Count; i++) Assert.That(result[i].EquivalentTo(expected[i]));
        }

        [Test]
        public void Should_return_user_by_id_same_as_from_repository()
        {
            // Arrange
            var userId = 0;
            var expected = UserDto.Create(_userRepository.Get(userId));

            // Act
            var result = _userService.Get(userId);

            // Assert
            Assert.That(result != null);
            Assert.That(result.EquivalentTo(expected));
        }
    }
}