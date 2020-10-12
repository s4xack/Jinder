using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface IUserService
    {
        List<UserDto> Get();
        UserDto Get(Int32 userId);
        UserDto Create(CreateUserDto user);
    }
}