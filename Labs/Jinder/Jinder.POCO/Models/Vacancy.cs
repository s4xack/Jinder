using System;
using System.Collections.Generic;
using System.Text;

namespace Jinder.Poco.Models
{
    public class Vacancy
    {
        public Int32 UserId { get; set;  }
        public Int32 Id { get; set; }
        public Specialization Specialization { get; }
        public List<Skill> Skills { get; }
        public String Information { get; }

        public Vacancy(Int32 userId, Specialization specialization, List<Skill> skills, String information)
        {
            UserId = userId;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }
    }
}
