﻿using System;
using System.Linq;
using Jinder.Api.Repository;
using Jinder.Api.Service;
using Jinder.Poco.Dto;
using NUnit.Framework;

namespace Jinder.Test.Service
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
        private IUserService _userService;
        private IUserRepository _userRepository;

        [SetUp]
        public void UserServiceSetUp()
        {
            _userRepository = new UserRepositoryMock();
            _userService = new UserService(_userRepository);
        }

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
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].EquivalentTo(expected[i]));
            }
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