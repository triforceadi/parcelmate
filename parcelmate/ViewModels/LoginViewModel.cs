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
    public partial class LoginViewModel : ObservableObject
    {
        private AuthenticationService _authService;
        [ObservableProperty]
        private bool _isLoggedIn;
        [ObservableProperty]
        private string _user;
        [ObservableProperty]
        private string _password;

        public LoginViewModel()
        {
            _authService = DependencyService.Resolve<AuthenticationService>();
        }

        [RelayCommand]
        private async void SignIn()
        {
            if (!string.IsNullOrWhiteSpace(User) && !string.IsNullOrWhiteSpace(Password))
            {
                bool auth = _authService.AuthenticateUser(User, Password);
                if (auth == true)
                {
                    await Application.Current.MainPage.DisplayAlert("Login Info", "Logged in succesfully", "OK");
                    Preferences.Set("IsLoggedInKey", true);
                    IsLoggedIn = true;
                    AppConstants.Username = User;
                    AppConstants.Password = Password;
                    Application.Current.MainPage = new AppShell();
                }
                else if(auth == false)
                {
                    await Application.Current.MainPage.DisplayAlert("Login Info", "Incorrect username or password", "OK");

                }
            }
            else if (string.IsNullOrWhiteSpace(User) && string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Login Info", "Please provide a username and password", "OK");

            }
        }

    }
}