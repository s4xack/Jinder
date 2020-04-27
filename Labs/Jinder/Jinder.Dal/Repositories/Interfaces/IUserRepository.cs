using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(Int32 userId);
    }
}