using System;
using System.Collections.Generic;
using System.Text;

namespace Jinder.Poco.Models
{
    public class Vacancy
    {
        public Int32 UserId { get; set; }
        public Int32 Id { get; set; }
        public Specialization Specialization { get; set; }
        public List<Skill> Skills { get; set; }
        public String Information { get; set; }
    }
}
