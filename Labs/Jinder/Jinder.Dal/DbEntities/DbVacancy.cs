using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.DbEntities
{
    public class DbVacancy
    {
        public Int32 Id { get; set; }

        [ForeignKey("User")]
        public Int32? UserId { get; set; }
        public DbUser User { get; set; }

        [ForeignKey("Specialization")] 
        public Int32 SpecializationId { get; set; }
        public DbSpecialization Specialization { get; set; }
        public List<DbVacancySkill> Skills { get; set; }
        public String Information { get; set; }

        public DbVacancy()
        {
        }

        public static DbVacancy FromModel(Vacancy vacancy)
        {
            return new DbVacancy()
            {
                Id = vacancy.Id,
                UserId = vacancy.User.Id,
                SpecializationId = vacancy.Specialization.Id,
                Skills = vacancy.Skills.Select(DbVacancySkill.FromModel).ToList(),
                Information = vacancy.Information
            };
        }

        public Vacancy ToModel()
        {
            return new Vacancy
            (
                User.ToModel(), 
                Specialization.ToModel(), 
                Skills.Select(s => s.Skill.ToModel()).ToList(), 
                Information
            ) {Id = Id};
        }
    }
}