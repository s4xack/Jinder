using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Jinder.Poco.Models;


namespace Jinder.Poco.Dto
{
    public class VacancyDto
    {
        public Int32 UserId { get; set; }
        public Int32 Id { get; set; }
        public String Specialization { get; set; }
        public List<String> Skills { get; set; }
        public String Information { get; set; }

        [JsonConstructor]
        public VacancyDto(Int32 userId, Int32 id, String specialization, List<String> skills, String information)
        {
            UserId = userId;
            Id = id;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }

        public static VacancyDto Create(Vacancy vacancy)
        {
            return new VacancyDto(
                vacancy.User.Id,
                vacancy.Id,
                vacancy.Specialization.Name,
                vacancy.Skills.Select(s => s.Skill.Name).ToList(),
                vacancy.Information);
        }

        public override Boolean Equals(Object obj)
        {
            return obj is VacancyDto other && (Id == other.Id &&
                                               UserId == other.UserId &&
                                               Skills.SequenceEqual(other.Skills) &&
                                               Specialization == other.Specialization &&
                                               Information == other.Information);
        }
    }
}