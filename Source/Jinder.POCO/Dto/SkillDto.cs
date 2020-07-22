using System;
using Jinder.Poco.Models;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class SkillDto
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public SkillDto()
        {
        }

        [JsonConstructor]
        public SkillDto(Int32 id, String name)
        {
            Id = id;
            Name = name;
        }

        public static SkillDto Create(Skill skill)
        {
            return new SkillDto(skill.Id, skill.Name);
        }
    }
}