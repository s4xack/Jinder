using System.Windows;
using System.Windows.Input;
using Jinder.Desktop.Commands;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Types;

namespace Jinder.Desktop.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public ICommand EnterAsCandidateCommand => new BaseCommand(arg => EnterAsCandidate());
        public ICommand EnterAsRecruiterCommand => new BaseCommand(arg => EnterAsRecruiter());

        private readonly IAuthenticationService _authenticationService;

        public StartViewModel()
        {
            _authenticationService = new AuthenticationServiceMock();
        }

        private void Enter()
        {
            var start = new Views.MainWindow();
            start.Show();
            
            var windows = Application.Current?.Windows;
            if (!(windows is null))
                foreach (Window currentWindow in Application.Current?.Windows )
                    if (currentWindow.Title == "Login") 
                        currentWindow.Close();
        }

        private void EnterAsCandidate()
        {
            Session.Token = _authenticationService.Authenticate(UserType.Candidate);
            Enter();
        }
        private void EnterAsRecruiter()
        {
            Session.Token = _authenticationService.Authenticate(UserType.Recruiter);
            Enter();
        }

    }
}