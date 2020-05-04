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
            foreach (var skill in _skills) _newId = Math.Max(_newId, skill.Id);
            _newId++;
        }

        public SkillRepositoryMock() : this(new List<Skill>())
        {
        }

        public List<Skill> GetAll()
        {
            return _skills;
        }

        public Skill Get(Int32 skillId)
        {
            return _skills.FirstOrDefault(s => s.Id == skillId) ??
                   throw new ArgumentException($"No skill with id {skillId}!");
        }

        public Skill GetByName(String skillName)
        {
            return _skills.FirstOrDefault(s => s.Name == skillName) ??
                   throw new ArgumentException($"No skill with id {skillName}!");
        }

        public Skill Add(Skill skill)
        {
            _skills.Add(skill);
            return skill;
        }

        public Skill Delete(Int32 skillId)
        {
            var skill = _skills.FirstOrDefault(s => s.Id == skillId) ??
                        throw new ArgumentException($"No skill with id {skillId}");
            _skills.Remove(skill);
            return skill;
        }

        public Int32 NewId { get => _newId++; }
    }
}