using System;
using System.Collections.Generic;
using Jinder.Poco.Dto;

namespace Jinder.Core.Services
{
    public interface IMatchService
    {
        List<SummaryDto> GetAllForRecruiter(Int32 userId);
        List<VacancyDto> GetAllForCandidate(Int32 userId);
        void UpdateMatch(Int32 summaryId, Int32 vacancyId);
    }
}