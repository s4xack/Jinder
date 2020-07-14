using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Dal.Repositories;
using Jinder.Poco.Dto;
using Jinder.Poco.Models;

namespace Jinder.Core.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        }

        public IReadOnlyCollection<SkillDto> GetAll()
        {
            return _skillRepository.GetAll()
                .Select(SkillDto.Create)
                .ToList();
        }

        public SkillDto Create(SkillDto skill)
        {
            Skill newSkill = _skillRepository.Add(new Skill(skill.Name));
            return SkillDto.Create(newSkill);
        }

        public SkillDto DeleteByName(String skillName)
        {
            return SkillDto.Create(_skillRepository.DeleteByName(skillName));
        }
    }
}