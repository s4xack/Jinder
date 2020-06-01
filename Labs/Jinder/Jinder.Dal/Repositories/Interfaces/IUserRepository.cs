using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface IUserRepository
    {
        IReadOnlyCollection<User> GetAll();
        User Get(Int32 userId);
    }
}