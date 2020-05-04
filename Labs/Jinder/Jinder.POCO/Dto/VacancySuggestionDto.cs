﻿using System;
using Jinder.Poco.Models;
using Jinder.Poco.Types;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class VacancySuggestionDto
    {
        public Int32 Id { get; }
        public VacancyDto Vacancy { get; }
        public SuggestionStatus Status { get; }

        [JsonConstructor]
        public VacancySuggestionDto(Int32 id, VacancyDto vacancy, SuggestionStatus status)
        {
            Id = id;
            Vacancy = vacancy;
            Status = status;
        }

        public static VacancySuggestionDto Create(VacancySuggestion vacancySuggestion)
        {
            return new VacancySuggestionDto(
                vacancySuggestion.Id,
                VacancyDto.Create(vacancySuggestion.Vacancy),
                vacancySuggestion.Status);
        }
    }
}