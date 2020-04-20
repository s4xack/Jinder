﻿using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class VacancyDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Specialization { get; set; }
        public List<string> Skills { get; set; }
        public string Information { get; set; }
    }
}