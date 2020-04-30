using System;

namespace Jinder.Poco.Models
{
    public class Skill
    {
        public Int32 Id { get; }
        public String Name { get; }

        public Skill(Int32 id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}