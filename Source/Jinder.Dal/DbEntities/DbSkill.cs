using System;
using Jinder.Poco.Models;

namespace Jinder.Dal.DbEntities
{
    public class DbSkill
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public DbSkill()
        {
        }

        public static DbSkill FromModel(Skill skill)
        {
            return new DbSkill()
            {
                Id = skill.Id,
                Name = skill.Name
            };
        }

        public Skill ToModel()
        {
            return new Skill(Name) {Id = Id};
        }

    }
}