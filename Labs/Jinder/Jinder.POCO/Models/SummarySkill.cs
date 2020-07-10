using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jinder.Poco.Models
{
    public class SummarySkill
    {
        [ForeignKey("Skill")] 
        public String SkillName { get; private set; }
        public Skill Skill { get; private set; }

        [ForeignKey("Summary")]
        public Int32 SummaryId { get; private set; }
        public Summary Summary { get; private set; }

        public SummarySkill()
        {
        }

        public SummarySkill(Skill skill, Summary summary)
        {
            Skill = skill;
            SkillName = skill.Name;

            Summary = summary;
            SummaryId = summary.Id;
        }
    }
}