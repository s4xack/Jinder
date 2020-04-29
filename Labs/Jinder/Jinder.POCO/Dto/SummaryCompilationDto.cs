using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jinder.Poco.Dto
{
    public class SummaryCompilationDto
    {
        public Int32 UserId { get; }
        public Int32 VacancyId { get; }
        public List<SummaryDto> Summaries { get; }

        [JsonConstructor]
        public SummaryCompilationDto(Int32 userId, Int32 vacancyId, List<SummaryDto> summaries)
        {
            UserId = userId;
            VacancyId = vacancyId;
            Summaries = summaries;
        }
    }
}