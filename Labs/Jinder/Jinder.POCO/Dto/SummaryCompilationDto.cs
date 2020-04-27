using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class SummaryCompilationDto
    {
        public Int32 UserId { get; set; }
        public Int32 VacancyId { get; set; }
        public List<SummaryDto> Summaries { get; set; }
    }
}