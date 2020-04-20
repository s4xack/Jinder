using System;
using System.Collections.Generic;

namespace Jinder.Poco.Dto
{
    class VacancyCompilationDto
    {
        public Guid UserId { get; set; }
        public Guid SummaryId { get; set; }
        public List<VacancyDto> Vacancies { get; set; }
    }
}
