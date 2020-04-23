using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class VacancyCompilationDto
    {
        public int UserId { get; set; }
        public int SummaryId { get; set; }
        public List<VacancyDto> Vacancies { get; set; }
    }
}
