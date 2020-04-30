using System;
using System.Collections.Generic;

namespace Jinder.Poco.Models
{
    public class Summary
    {
        public Int32 UserId { get; }
        public Int32 Id { get; }
        public Specialization Specialization { get; }
        public List<Skill> Skills { get; }
        public String Information { get; }

        public Summary(Int32 userId, Int32 id, Specialization specialization, List<Skill> skills, String information)
        {
            UserId = userId;
            Id = id;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }
    }
}