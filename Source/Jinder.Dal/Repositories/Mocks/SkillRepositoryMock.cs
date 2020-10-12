using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.Repositories.Mocks
{
    public class SkillRepositoryMock : ISkillRepository
    {
        private readonly List<Skill> _skills;
        private Int32 _newId;

        public SkillRepositoryMock(List<Skill> skills)
        {
            _skills = skills;
            _newId = 0;
            foreach (var skill in _skills) 
                skill.Id = _newId++;

        }

        public SkillRepositoryMock() : this(new List<Skill>())
        {
        }

        public IReadOnlyCollection<Skill> Get()
        {
            return _skills;
        }


        public Skill GetByName(String skillName)
        {
            return _skills.FirstOrDefault(s => s.Name == skillName) ??
                   throw new ArgumentException($"No skill with id {skillName}!");
        }

        public Skill Add(Skill skill)
        {
            skill.Id = _newId++;
            _skills.Add(skill);
            return skill;
        }

        public Skill DeleteByName(String skillName)
        {
            var skill = _skills.FirstOrDefault(s => s.Name == skillName) ??
                        throw new ArgumentException($"No skill with id {skillName}");
            _skills.Remove(skill);
            return skill;
        }
    }
}