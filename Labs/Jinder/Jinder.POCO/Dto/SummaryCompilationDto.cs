using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class SummaryCompilationDto
    {
        public Guid UserId { get; set; }
        public Guid VacancyId { get; set; }
        public List<SummaryDto> Summaries { get; set; }
    }
}
