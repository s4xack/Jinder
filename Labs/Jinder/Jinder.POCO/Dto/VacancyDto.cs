﻿using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class VacancyDto
    {
        public Int32 UserId { get; set; }
        public Int32 Id { get; set; }
        public String Specialization { get; set; }
        public List<String> Skills { get; set; }
        public String Information { get; set; }
    }
}