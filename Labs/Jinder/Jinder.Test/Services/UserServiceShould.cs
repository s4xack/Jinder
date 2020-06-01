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
                new User(
                    "0@email.com",
                    "Admin",
                    string.Empty,
                    UserType.Administrator),
                new User(
                    "1@email.com",
                    "Ivan",
                    string.Empty,
                    UserType.Candidate),
                new User(
                    "2@email.com",
                    "Shepherd",
                    string.Empty,
                    UserType.Recruiter),
                new User(
                    "3@email.com",
                    "Alex",
                    string.Empty,
                    UserType.Candidate),
                new User(
                    "4@email.com",
                    "Max",
                    string.Empty,
                    UserType.Candidate),
                new User(
                    "5@email.com",
                    "Roman",
                    string.Empty,
                    UserType.Recruiter),
                new User(
                    "6@email.com",
                    "Nick",
                    string.Empty,
                    UserType.Recruiter)
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