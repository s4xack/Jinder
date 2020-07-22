using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Mock
{
    public static class Content
    {
        public static List<SummaryDto> Summaries { get; } = new List<SummaryDto>()
        {
            new SummaryDto(1, 1, "Game developer",
                new List<String>() {".net", "unity", "cross-eyed"}, "Ivan does not need a summary. Ivan is too cool."),
            new SummaryDto(2, 2, "Junior .net developer",
                new List<String>() {".net", "asp", "ado"}, "Some guy with cool skills."),
        };

        public static List<VacancyDto> Vacancies { get; } = new List<VacancyDto>()
        {
            new VacancyDto(1, 1, "Veeam Academy student",
                new List<String>() {".net", "db", "bcl"}, "Evening free Intensive C# for beginners with the prospect of employment at Veeam Software for the best students."),
            new VacancyDto(2, 2, "Mail.ru Games",
                new List<String>() {".net", "unity", "android"}, "Ivan does not need a summary. Ivan is too cool."),
        };

    }
}