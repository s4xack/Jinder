using System;
using Jinder.Poco.Models;

namespace Jinder.Dal.DbEntities
{
    public class DbSpecialization
    {
        
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public DbSpecialization()
        {
        }

        public static DbSpecialization FromModel(Specialization specialization)
        {
            return new DbSpecialization()
            {
                Id = specialization.Id,
                Name = specialization.Name
            };
        }

        public Specialization ToModel()
        {
            return new Specialization(Name) {Id = Id};
        }

    }
}