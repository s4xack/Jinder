﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Jinder.Poco.Models;


namespace Jinder.Poco.Dto
{
    public class VacancyDto
    {
        public Int32 UserId { get; }
        public Int32 Id { get; }
        public String Specialization { get; }
        public List<String> Skills { get; }
        public String Information { get; }

        [JsonConstructor]
        public VacancyDto(Int32 userId, Int32 id, String specialization, List<String> skills, String information)
        {
            UserId = userId;
            Id = id;
            Specialization = specialization;
            Skills = skills;
            Information = information;
        }

        public static VacancyDto Create(Vacancy vacancy)
        {
            return new VacancyDto(
                vacancy.UserId,
                vacancy.Id,
                vacancy.Specialization.Name,
                vacancy.Skills.Select(s => s.Name).ToList(),
                vacancy.Information);
        }
    }
}