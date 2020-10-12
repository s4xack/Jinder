using System;
using System.Collections.Generic;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories
{
    public interface ISkillRepository
    {
        IReadOnlyCollection<Skill> Get();
        Skill GetByName(String skillName);
        Skill Add(Skill skill);
        Skill DeleteByName(String skillName);
    }
}