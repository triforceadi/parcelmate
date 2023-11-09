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
    public partial class CourierInfoViewModel : ObservableObject
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
        }

        [RelayCommand]
        private void SignOut()
        {
            IsLoggedIn = false;
            Preferences.Set("IsLoggedInKey", false);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

    }
}