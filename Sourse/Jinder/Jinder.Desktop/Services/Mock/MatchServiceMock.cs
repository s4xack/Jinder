using System;
using System.Collections.Generic;
using System.Linq;
using Jinder.Desktop.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.Services.Mock
{
    public class MatchServiceMock : IMatchService
    {
        public List<VacancyDto> GetAllForCandidate(Guid token)
        {
            var result = new List<VacancyDto>();
            if (Recruiter.LikeTest && Candidate.LikeTest)
                result.Add(Recruiter.Vacancy);
            return result;
        }

        public List<SummaryDto> GetAllForRecruiter(Guid token)
        {
            var result = new List<SummaryDto>();
            if (Recruiter.LikeTest && Candidate.LikeTest)
                result.Add(Candidate.Summary);
            return result;
        }
    }
}