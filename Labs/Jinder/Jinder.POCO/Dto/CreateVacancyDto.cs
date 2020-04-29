using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class CreateVacancyDto
    {
        public String Specialization { get; }
        public List<String> Skills { get; }
        public String Information { get; }

        [JsonConstructor]
        public CreateVacancyDto(String specialization, List<String> skills, String information)
        {
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }
    }
}