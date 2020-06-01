using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface ISkillRepository
    {
        IReadOnlyCollection<Skill> GetAll();
        Skill Get(Int32 skillId);
        Skill GetByName(String skillName);
        Skill Add(Skill skill);
        Skill Delete(Int32 skillId);
        Int32 NewId { get; }
    }
}