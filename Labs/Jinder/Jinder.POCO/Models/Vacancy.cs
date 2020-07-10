using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jinder.Poco.Models
{
    public class Vacancy
    {
        public User User { get; private set; }
        public Int32 Id { get; set; }
        public Specialization Specialization { get; private set; }
        public List<VacancySkill> Skills { get; private set; }
        public String Information { get; private set; }

        public Vacancy()
        {
        }

        public Vacancy(User user, Specialization specialization, List<Skill> skills, String information)
        {
            User = user;
            Specialization = specialization;
            Skills = skills.Select(s => new VacancySkill(s, this)).ToList();
            Information = information;
        }
    }
}
