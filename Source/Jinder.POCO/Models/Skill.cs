using System;

namespace Jinder.Poco.Models
{
    public class Skill
    {
        public Int32 Id { get; set; }
        public String Name { get; }

        public Skill(String name)
        {
            Name = name;
        }

        public override Boolean Equals(Object obj)
        {
            return obj is Skill other && Id == other.Id;
        }
    }
}