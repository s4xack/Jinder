﻿using System;

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
    }
}