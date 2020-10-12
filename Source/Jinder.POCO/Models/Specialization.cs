using System;

namespace Jinder.Poco.Models
{
    public class Specialization
    {
        public Int32 Id { get; set; }
        public String Name { get; }

        public Specialization(String name)
        {
            Name = name;
        }

        public override Boolean Equals(Object obj)
        {
            return obj is Specialization other && Id == other.Id;
        }
    }
}