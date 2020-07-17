using System.Windows.Input;
using Jinder.Desktop.Commands;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class VacancySuggestionsViewModel : BaseViewModel
    {
        public ICommand AcceptCommand { get; }
        public ICommand SkipCommand { get; }
        public ICommand RejectCommand { get; }

        private VacancySuggestionDto _vacancySuggestion;

        public VacancySuggestionDto VacancySuggestion
        {
            get => _vacancySuggestion;
            set
            {
                _vacancySuggestion = value;
                OnPropertyChanged();
            }
        }

        private readonly IVacancySuggestionService _vacancySuggestionService;

        public VacancySuggestionsViewModel()
        {
            _vacancySuggestionService = new VacancySuggestionService();
            VacancySuggestion = _vacancySuggestionService.GetForMe(Session.Token);

            AcceptCommand = new BaseCommand(arg =>
            {
                _vacancySuggestionService.Accept(Session.Token, _vacancySuggestion.Id);
                VacancySuggestion = _vacancySuggestionService.GetForMe(Session.Token);
            });

            SkipCommand = new BaseCommand(arg =>
            {
                _vacancySuggestionService.Skip(Session.Token, _vacancySuggestion.Id);
                VacancySuggestion = _vacancySuggestionService.GetForMe(Session.Token);
            });

            RejectCommand = new BaseCommand(arg =>
            {
                _vacancySuggestionService.Reject(Session.Token, _vacancySuggestion.Id);
                VacancySuggestion = _vacancySuggestionService.GetForMe(Session.Token);
            });
        }
    }
}