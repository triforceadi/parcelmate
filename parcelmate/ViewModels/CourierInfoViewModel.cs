using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using parcelmate.Constants;
using parcelmate.Models;
using parcelmate.Services;
using parcelmate.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace parcelmate.ViewModels
{
    public partial class  CourierInfoViewModel : ObservableObject
    {
        private AuthenticationService _authService;
        private CourierDataService _courierDataService;
        [ObservableProperty]
        private bool _isLoggedIn;
        [ObservableProperty]
        private string _user;
        [ObservableProperty]
        private string _password;
        public CourierInfoViewModel()
        {
            _authService = DependencyService.Resolve<AuthenticationService>();
            _courierDataService = DependencyService.Resolve<CourierDataService>();
           // IsLoggedIn = Preferences.Get(AppConstants.IsLoggedInKey, true);
        }

        [RelayCommand]
        private void SignOut()
        {
            IsLoggedIn = false;
            Preferences.Set(AppConstants.IsLoggedInKey, false);
            //IsLoggedIn = false;
        }
        [RelayCommand]
        private void SignIn()
        {
            if(!string.IsNullOrWhiteSpace(User)&& !string.IsNullOrWhiteSpace(Password))
            {
                bool auth = _authService.AuthenticateUser(User, Password);
                if(auth == true)
                {
                    IsLoggedIn = true;
                    Preferences.Set(AppConstants.IsLoggedInKey, true);
                }
            }
        }
    }
}