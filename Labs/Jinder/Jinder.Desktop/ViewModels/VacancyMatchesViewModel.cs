using System.Collections.Generic;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class VacancyMatchesViewModel
    {
        public List<VacancyDto> Vacancies { get; set; }

        private IMatchService _matchService;

        public VacancyMatchesViewModel()
        {
            _matchService = new MatchServiceMock();
            Vacancies = _matchService.GetAllForCandidate(Session.Token);
        }
    }
}