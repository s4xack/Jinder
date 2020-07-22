using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Jinder.Poco.Models
{
    public class Summary
    {
        public User User { get; }
        public Int32 Id { get; set; }
        public Specialization Specialization { get; }
        public List<Skill> Skills { get; }
        public String Information { get; }

        public Summary(User user, Specialization specialization, List<Skill> skills, String information)
        {
            User = user;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }
    }
}