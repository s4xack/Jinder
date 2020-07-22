using System.Windows.Controls;
using Jinder.Desktop.ViewModels;
using Jinder.Desktop.Views;

namespace Jinder.Desktop
{
    public static class PageNavigator
    {
        private static MenuViewModel _menuViewModel;

        public static void Register(MenuViewModel mainWindowViewModel)
        {
            _menuViewModel = mainWindowViewModel;
        }

        public static void NavigateTo(Page page)
        {
            _menuViewModel.CurrentPage = page;
        }
    }
}