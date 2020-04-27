using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Poco.Dto
{
    public class SummaryDto
    {
        public SummaryDto()
        {
        }

        private SummaryDto(Int32 userId, Int32 id, String specialization, List<String> skills, String information)
        {
            UserId = userId;
            Id = id;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }

        public Int32 UserId { get; set; }
        public Int32 Id { get; set; }
        public String Specialization { get; set; }
        public List<String> Skills { get; set; }
        public String Information { get; set; }

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