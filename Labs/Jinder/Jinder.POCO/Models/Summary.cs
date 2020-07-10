using System;
using System.Collections.Generic;
using System.Linq;

namespace Jinder.Poco.Models
{
    public class Summary
    {
        public User User { get; private set; }
        public Int32 Id { get; set; }
        public Specialization Specialization { get; private set; }
        public List<SummarySkill> Skills { get; private set; }
        public String Information { get; private set; }

        public Summary()
        {
        }

        public Summary(User user, Specialization specialization, List<Skill> skills, String information)
        {
            User = user;
            Specialization = specialization;
            Skills = skills.Select(s => new SummarySkill(s, this)).ToList();
            Information = information;
        }
    }
}