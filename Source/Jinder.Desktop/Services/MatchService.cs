using System;
using System.Collections.Generic;
using Jinder.Desktop.Clients;
using Jinder.Poco.Dto;
using Refit;

namespace Jinder.Desktop.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchClient _matchClient;

        public MatchService()
        {
            _matchClient = RestService.For<IMatchClient>(Session.HostUrl);
        }

        public List<VacancyDto> GetAllForCandidate(Guid token)
        {
            return _matchClient.GetAllForCandidate(token).Result;
        }

        public List<SummaryDto> GetAllForRecruiter(Guid token)
        {
            return _matchClient.GetAllForRecruiter(token).Result;
        }
    }
}