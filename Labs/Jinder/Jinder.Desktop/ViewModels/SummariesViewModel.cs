using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class SummariesViewModel : BaseViewModel
    {
        private SummaryDto _summary;

        public SummaryDto Summary
        {
            get => _summary;
            set
            {
                _summary = value;
                OnPropertyChanged();
            }
        }

        private readonly ISummaryService _summaryService;

        public SummariesViewModel()
        {
            _summaryService = new SummaryServiceMock();
            _summary = _summaryService.GetForMe(Session.Token);
        }
    }
}