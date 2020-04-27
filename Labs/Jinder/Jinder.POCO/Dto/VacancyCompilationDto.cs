using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    public class VacancyCompilationDto
    {
        public Int32 UserId { get; set; }
        public Int32 SummaryId { get; set; }
        public List<VacancyDto> Vacancies { get; set; }
    }
}