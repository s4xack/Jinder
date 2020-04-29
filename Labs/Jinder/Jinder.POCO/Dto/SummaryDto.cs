using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Jinder.Poco.Models;

namespace Jinder.Poco.Dto
{
    public class SummaryDto
    {
        public Int32 UserId { get; }
        public Int32 Id { get; }
        public String Specialization { get; }
        public List<String> Skills { get; }
        public String Information { get; }

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
                summary.UserId,
                summary.Id,
                summary.Specialization.Name,
                summary.Skills.Select(s => s.Name).ToList(),
                summary.Information);
        }
    }
}