using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Jinder.Desktop.Commands;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Desktop.Views;
using Jinder.Poco.Dto;
using Jinder.Poco.Types;

namespace Jinder.Desktop.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand UserCommand { get; } = new BaseCommand(arg => PageNavigator.NavigateTo(new UserPage()));
        public ICommand MatchesCommand { get; }
        public ICommand RequestsCommand { get; } 
        public ICommand SuggestionCommand { get; }

        public ICommand BackToStartCommand => new BaseCommand(arg =>
        {
            var start = new Views.StartWindow();
            start.Show();

            var windows = Application.Current?.Windows;
            if (!(windows is null))
                foreach (Window currentWindow in Application.Current?.Windows )
                    if (currentWindow.Title == "Jinder") 
                        currentWindow.Close();
        });

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private readonly IUserService _userService;
        
        public MenuViewModel()
        {
            _userService = new UserService();
            UserDto user = _userService.GetMe(Session.Token);

            if (user.Type == UserType.Candidate)
            {
                RequestsCommand = new BaseCommand(arg => PageNavigator.NavigateTo(new SummariesPage()));
                SuggestionCommand = new BaseCommand(arg => PageNavigator.NavigateTo(new VacancySuggestionsPage()));
                MatchesCommand = new BaseCommand(arg => PageNavigator.NavigateTo(new VacancyMatchesPage()));
            } 
            else if (user.Type == UserType.Recruiter)
            {
                RequestsCommand = new BaseCommand(arg => PageNavigator.NavigateTo(new VacanciesPage()));
                SuggestionCommand = new BaseCommand(arg => PageNavigator.NavigateTo(new SummarySuggestionsPage()));
                MatchesCommand = new BaseCommand(arg => PageNavigator.NavigateTo(new SummaryMatchesPage()));
            }
            else
            {
                throw new ArgumentException();
            }
            
            PageNavigator.Register(this);
            SuggestionCommand.Execute(null);
        }

    }
}
