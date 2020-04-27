using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Api.Service
{
    public interface IUserService
    {
        public List<UserDto> GetAll();
        public UserDto Get(Int32 userId);
    }
}