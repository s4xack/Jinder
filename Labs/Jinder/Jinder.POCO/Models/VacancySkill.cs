using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jinder.Poco.Models
{
    public class VacancySkill
    {
        [ForeignKey("Skill")] 
        public String SkillName { get; private set; }
        public Skill Skill { get; private set; }

        [ForeignKey("Vacancy")] 
        public Int32 VacancyId { get; private set; }
        public Vacancy Vacancy { get; private set; }

        public VacancySkill()
        {
        }

        public VacancySkill(Skill skill, Vacancy vacancy)
        {
            Skill = skill;
            SkillName = skill.Name;

            Vacancy = vacancy;
            VacancyId = vacancy.Id;
        }
    }
}