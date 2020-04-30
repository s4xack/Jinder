using System;

namespace Jinder.Poco.Models
{
    public class Specialization
    {
        public Int32 Id { get; }
        public String Name { get; }

        public Specialization(Int32 id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}