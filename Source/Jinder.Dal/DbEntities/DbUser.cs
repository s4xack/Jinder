using System;
using Jinder.Poco.Models;
using Jinder.Poco.Types;

namespace Jinder.Dal.DbEntities
{
    public class DbUser
    {
        public Int32 Id { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public UserType Type { get; set; }

        public DbUser()
        {
        }

        public static DbUser FromModel(User user)
        {
            return new DbUser()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Type = user.Type
            };
        }

        public User ToModel()
        {
            return new User(Email, Name, Type) {Id = Id};
        }

    }
}