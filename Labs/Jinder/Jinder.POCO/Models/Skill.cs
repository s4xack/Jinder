using System;
using System.ComponentModel.DataAnnotations;

namespace Jinder.Poco.Models
{
    public class Skill
    {
        [Key]
        public String Name { get; private set; }

        public Skill()
        {
        }

        public Skill(String name)
        {
            Name = name;
        }
    }
}