using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class VacanciesViewModel : BaseViewModel
    {
        private VacancyDto _vacancy;

        public VacancyDto Vacancy
        {
            get => _vacancy;
            set
            {
                _vacancy = value;
                OnPropertyChanged();
            }
        }

        private readonly IVacancyService _vacancyService;

        public VacanciesViewModel()
        {
            _vacancyService = new VacancyServiceMock();
            _vacancy = _vacancyService.GetForMe(Session.Token);
        }
    }
}