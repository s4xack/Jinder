using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Jinder.Poco.Models;

namespace Jinder.Poco.Dto
{
    public class SummaryDto
    {
        public Int32 UserId { get; set; }
        public Int32 Id { get; set; }
        public String Specialization { get; set; }
        public List<String> Skills { get; set; }
        public String Information { get; set; }

        [JsonConstructor]
        public SummaryDto(Int32 userId, Int32 id, String specialization, List<String> skills, String information)
        {
            UserId = userId;
            Id = id;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }

        public static SummaryDto Create(Summary summary)
        {
            return new SummaryDto(
                summary.User.Id,
                summary.Id,
                summary.Specialization.Name,
                summary.Skills.Select(s => s.Name).ToList(),
                summary.Information);
        }

        public override Boolean Equals(Object obj)
        {
            return obj is SummaryDto other && (Id == other.Id &&
                                               UserId == other.UserId &&
                                               Skills.SequenceEqual(other.Skills) &&
                                               Specialization == other.Specialization &&
                                               Information == other.Information);
        }
    }
}