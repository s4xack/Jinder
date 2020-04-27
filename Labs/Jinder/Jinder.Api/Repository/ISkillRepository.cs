using System;
using System.Collections.Generic;
using Jinder.Poco.Model;

namespace Jinder.Api.Repository
{
    public interface ISkillRepository
    {
        public List<Skill> GetAll();
        public Skill Get(Int32 skillId);
        public Skill GetByName(String skillName);
        public Skill Add(Skill skill);
        public Skill Delete(Int32 skillId);
    }
}