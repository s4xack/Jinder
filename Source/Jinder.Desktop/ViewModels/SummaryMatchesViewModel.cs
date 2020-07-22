using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class SummaryMatchesViewModel : BaseViewModel
    {
        public List<SummaryDto> Summaries { get; set; }

        private IMatchService _matchService;

        public SummaryMatchesViewModel()
        {
            _matchService = new MatchService();
            Summaries = _matchService.GetAllForRecruiter(Session.Token);
        }
    }
}