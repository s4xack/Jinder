using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class SummaryCompilationDto
    {
        public int UserId { get; set; }
        public int VacancyId { get; set; }
        public List<SummaryDto> Summaries { get; set; }
    }
}
