using System;
using System.Collections.Generic;
using Jinder.Poco.Model;

namespace Jinder.Api.Repository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public User Get(Int32 userId);
    }
}