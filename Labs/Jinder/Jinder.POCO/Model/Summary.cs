using System;
using System.Collections.Generic;

namespace Jinder.Poco.Model
{
    public class Summary
    {
        public Int32 UserId { get; set; }
        public Int32 Id { get; set; }
        public Specialization Specialization { get; set; }
        public List<Skill> Skills { get; set; }
        public String Information { get; set; }
    }
}