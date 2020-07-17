using System;
using System.ComponentModel.DataAnnotations.Schema;
using Jinder.Poco.Models;

namespace Jinder.Dal.DbEntities
{
    public class DbSummarySkill
    {
        [ForeignKey("Summary")]
        public Int32 SummaryId { get; set; }
        public DbSummary Summary { get; set; }

        [ForeignKey("Skill")]
        public Int32 SkillId { get; set; }
        public DbSkill Skill { get; set; }

        public DbSummarySkill()
        {
        }

        public static DbSummarySkill FromModel(Skill skill)
        {
            return new DbSummarySkill()
            {
                SkillId = skill.Id
            };
        }
    }
}