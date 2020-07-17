using System;
using System.ComponentModel.DataAnnotations.Schema;
using Jinder.Poco.Models;

namespace Jinder.Dal.DbEntities
{
    public class DbVacancySkill
    {
        [ForeignKey("Vacancy")]
        public Int32 VacancyId { get; set; }
        public DbVacancy Vacancy { get; set; }

        [ForeignKey("Skill")]
        public Int32 SkillId { get; set; }
        public DbSkill Skill { get; set; }

        public DbVacancySkill()
        {
        }

        public static DbVacancySkill FromModel(Skill skill)
        {
            return new DbVacancySkill()
            {
                SkillId = skill.Id
            };
        }
    }
}