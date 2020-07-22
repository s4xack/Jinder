using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Jinder.Poco.Models;

namespace Jinder.Dal.DbEntities
{
    public class DbSummary
    {
        public Int32 Id { get; set; }

        [ForeignKey("User")]
        public Int32? UserId { get; set; }
        public DbUser User { get; set; }

        [ForeignKey("Specialization")] 
        public Int32 SpecializationId { get; set; }
        public DbSpecialization Specialization { get; set; }

        public List<DbSummarySkill> Skills { get; set; }
        public String Information { get; set; }

        public DbSummary()
        {
        }

        public static DbSummary FromModel(Summary summary)
        {
            return new DbSummary()
            {
                Id = summary.Id,
                UserId = summary.User.Id,
                SpecializationId = summary.Specialization.Id,
                Skills = summary.Skills.Select(DbSummarySkill.FromModel).ToList(),
                Information = summary.Information
            };
        }

        public Summary ToModel()
        {
            return new Summary
            (
                User.ToModel(), 
                Specialization.ToModel(), 
                Skills.Select(s => s.Skill.ToModel()).ToList(), 
                Information
                ) {Id = Id};
        }
    }
}