﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class CreateSummaryDto
    {
        public String Specialization { get; set; }
        public List<String> Skills { get; set; }
        public String Information { get; set; }

        public CreateSummaryDto()
        {
        }

        [JsonConstructor]
        public CreateSummaryDto(String specialization, List<String> skills, String information)
        {
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }
    }
}