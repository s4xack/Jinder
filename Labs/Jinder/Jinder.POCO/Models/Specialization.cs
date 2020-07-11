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
    }
}