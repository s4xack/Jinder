using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Jinder.Desktop.Commands;
using Jinder.Desktop.Views;

namespace Jinder.Desktop.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand UserCommand { get; set; } = new BaseCommand(arg => PageNavigator.NavigateTo(new UserPage()));
        public ICommand RequestsCommand { get; set; } = new BaseCommand(arg => PageNavigator.NavigateTo(new RequestsPage()));
        public ICommand SuggestionCommand { get; set; } = new BaseCommand(arg => PageNavigator.NavigateTo(new SuggestionsPage()));
        public ICommand MatchesCommand { get; set; } = new BaseCommand(arg => PageNavigator.NavigateTo(new MatchesPage()));

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
        
        public MenuViewModel()
        {
            _currentPage = new SuggestionsPage();
            PageNavigator.Register(this);
        }
    }
}
