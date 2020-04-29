using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class VacancyCompilationDto
    {
        public Int32 UserId { get; }
        public Int32 SummaryId { get; }
        public List<VacancyDto> Vacancies { get; }

        [JsonConstructor]
        public VacancyCompilationDto(Int32 userId, Int32 summaryId, List<VacancyDto> vacancies)
        {
            UserId = userId;
            SummaryId = summaryId;
            Vacancies = vacancies;
        }
    }
}