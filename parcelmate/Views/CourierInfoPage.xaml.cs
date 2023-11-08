using parcelmate.Constants;
using parcelmate.Models;
using parcelmate.Services;
using parcelmate.ViewModels;
using parcelmate.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace parcelmate.Views
{
    public partial class CourierInfoPage : ContentPage
    {
        private AuthenticationService authService;
        private CourierDataService courierDataService;
        private string username;
        private string password;
        public CourierInfoViewModel ViewModel
        {
            get { return (CourierInfoViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public CourierInfoPage()
        {
            InitializeComponent();
            authService = new AuthenticationService();
            courierDataService = new CourierDataService();
            ViewModel = new CourierInfoViewModel();
            if (Settings.FirstRun)
            {
                Preferences.Set(AppConstants.IsLoggedInKey, false);
                Settings.FirstRun = false;
            }
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                LoginFields.IsVisible = false;
                CourierDetails.IsVisible = true;
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            courierDataService.InitializeData();
            authService.InitializeData();
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                PopulateFields();
            }
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            username = UsernameEntry.Text;
            password = PasswordEntry.Text;

        bool isAuthenticated = authService.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                Preferences.Set(AppConstants.IsLoggedInKey, true);
                DisplayAlert("Login", "Login successful", "OK");
                LoginFields.IsVisible = false;
                CourierDetails.IsVisible = true;
                PopulateFields();

            }
            else
            {
                DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }

        private async void OnSignOutButtonClicked(object sender, EventArgs e)
        {
            if(Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                var action = await DisplayActionSheet("Are you sure you want to sign out?", "No", "Yes");

                if (action == "Yes")
                {
                   // ViewModel.SignOut();
                    LoginFields.IsVisible = true;
                    UsernameEntry.Text = string.Empty;
                    PasswordEntry.Text = string.Empty;
                    CourierDetails.IsVisible = false;
                    FirstName.Text = string.Empty;
                    LastName.Text = string.Empty;
                    Age.Text = string.Empty;
                    DriverLicenseExpiryDate.Text = string.Empty;
                    DriverLicenseCategory.Text = string.Empty;
                    IsCertified.Text = string.Empty;
                    IsDangerousGoodsAllowed.Text = string.Empty;
                }
            }
            else
            {
                await DisplayAlert("Sign out","You must be logged in first", "OK");
            }

        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
           
        }

        private void PopulateFields(object sender, EventArgs e)
        {
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                try
                {
                    int userId = authService.ReturnUserIdByUsername(username);
                    Courier courier = courierDataService.GetCourierById(userId);

                    FirstName.Text = courier.firstName;
                    LastName.Text = courier.surname;
                    Age.Text = courier.age;
                    DriverLicenseExpiryDate.Text = courier.driverLicenseExpiryDate;
                    DriverLicenseCategory.Text = courier.driverLicenseCategory;
                    IsCertified.Text = courier.isCertified;
                    IsDangerousGoodsAllowed.Text = courier.isAllowedDangerousGoods;

                    DisplayAlert("Success", "Data has been refreshed", "OK");
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", "Could not refresh data, try logging in again", "OK");
                }


            }
        }

        private void PopulateFields()
        {
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                try
                {
                    int userId = authService.ReturnUserIdByUsername(username);
                Courier courier = courierDataService.GetCourierById(userId);

                FirstName.Text = courier.firstName;
                LastName.Text = courier.surname;
                Age.Text = courier.age;
                DriverLicenseExpiryDate.Text = courier.driverLicenseExpiryDate;
                DriverLicenseCategory.Text = courier.driverLicenseCategory;
                IsCertified.Text = courier.isCertified;
                IsDangerousGoodsAllowed.Text = courier.isAllowedDangerousGoods;
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", "Could not populate data, try logging in again", "OK");
                }
            }
        }
    }
}