using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Api.Repository;
using Jinder.Poco.Dto;

namespace Jinder.Api.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        }

        public List<UserDto> GetAll()
        {
            return _userRepository
                .GetAll()
                .Select(UserDto.Create)
                .ToList();
        }

        public UserDto Get(Int32 userId)
        {
            return UserDto.Create(_userRepository.Get(userId));
        }
    }
}