using System;
using System.ComponentModel.DataAnnotations;

namespace Jinder.Poco.Models
{
    public class Specialization
    {
        [Key]
        public String Name { get; private set; }

        public Specialization()
        {
        }

        public Specialization(String name)
        {
            Name = name;
        }
    }
}