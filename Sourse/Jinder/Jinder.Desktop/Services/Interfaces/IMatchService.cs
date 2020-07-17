using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services
{
    public interface IMatchService
    {
        List<VacancyDto> GetAllForCandidate(Guid token);
        List<SummaryDto> GetAllForRecruiter(Guid token);
    }
}
