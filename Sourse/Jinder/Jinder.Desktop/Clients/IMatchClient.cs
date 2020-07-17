using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Clients
{
    public interface IMatchClient
    {
        [Get("/api/match/candidate/get/all/")]
        Task<List<VacancyDto>> GetAllForCandidate([Header("token")] Guid token);

        [Get("/api/match/recruiter/get/all/")]
        Task<List<SummaryDto>> GetAllForRecruiter([Header("token")] Guid token);
    }
}