using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jinder.Desktop.Mock;
using Jinder.Desktop.Services;
using Jinder.Desktop.Services.Mock;
using Jinder.Poco.Dto;

namespace Jinder.Desktop.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private UserDto _user;

        public UserDto User
        {
            get => _user;
            set 
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private readonly IUserService _userService;

        public UserViewModel()
        {
            _userService = new UserService();
            _user = _userService.GetMe(Session.Token);
        }
    }
}
