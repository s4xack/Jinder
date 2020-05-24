using System.Windows.Input;
using Jinder.Desktop.Commands;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class SummarySuggestionsViewModel : BaseViewModel
    {
        public ICommand AcceptCommand { get; } 
        public ICommand SkipCommand { get; } 
        public ICommand RejectCommand { get; } 

        private SummarySuggestionDto _summarySuggestion;
        public SummarySuggestionDto SummarySuggestion
        {
            get => _summarySuggestion;
            set
            {
                _summarySuggestion = value;
                OnPropertyChanged();
            }
        }

        private readonly ISummarySuggestionService _summarySuggestionService;

        public SummarySuggestionsViewModel()
        {
            _summarySuggestionService = new SummarySuggestionServiceMock();
            SummarySuggestion = _summarySuggestionService.GetForMe(Session.Token);

            AcceptCommand = new BaseCommand(arg =>
            {
                _summarySuggestionService.Accept(Session.Token, _summarySuggestion.Id);
                SummarySuggestion = _summarySuggestionService.GetForMe(Session.Token);
            });

            SkipCommand = new BaseCommand(arg =>
            {
                _summarySuggestionService.Skip(Session.Token, _summarySuggestion.Id);
                SummarySuggestion = _summarySuggestionService.GetForMe(Session.Token);
            });

            RejectCommand = new BaseCommand(arg =>
            {
                _summarySuggestionService.Reject(Session.Token, _summarySuggestion.Id);
                SummarySuggestion = _summarySuggestionService.GetForMe(Session.Token);
            });
        }
    }
}