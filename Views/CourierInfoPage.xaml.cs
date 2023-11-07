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
            PopulateFields();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            bool isAuthenticated = authService.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                Preferences.Set(AppConstants.IsLoggedInKey, true);
                DisplayAlert("Login", "Login successful", "OK");
                LoginFields.IsVisible = false;
                CourierDetails.IsVisible = true;

            }
            else
            {
                DisplayAlert("Error", "Username and password are required", "OK");
            }
        }

        private async void OnSignOutButtonClicked(object sender, EventArgs e)
        {
            if(Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                var action = await DisplayActionSheet("Are you sure you want to sign out?", "No", "Yes");

                if (action == "Yes")
                {
                    ViewModel.SignOut();
                    LoginFields.IsVisible = true;
                    UsernameEntry.Text = string.Empty;
                    PasswordEntry.Text = string.Empty;
                    CourierDetails.IsVisible = false;
                }
            }
            else
            {
                await DisplayAlert("Sign out","You must be logged in first", "OK");
            }

        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            Courier courier = new Courier()
            {
                firstName = FirstName.Text,
                lastName = LastName.Text,
                age = Age.Text,
                driverLicenseExpiryDate = DriverLicenseExpiryDate.Text,
                driverLicenseCategory = DriverLicenseCategory.Text,
                isAllowedDangerousGoods = IsDangerousGoodsAllowed.Text,
                isCertified = IsCertified.Text,
            };

            bool isSaved = await courierDataService.SaveCourierAsync(courier);

            if (isSaved)
            {
                await DisplayAlert("Save", "Data saved successfully", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to save data", "OK");
            }
        }

        private void PopulateFields()
        {
            Courier lastSavedCourier = courierDataService.GetLastSavedCourier();

            if (lastSavedCourier != null)
            {
                // Populate the fields with the retrieved data
                FirstName.Text = lastSavedCourier.firstName;
                LastName.Text = lastSavedCourier.lastName;
                Age.Text = lastSavedCourier.age;
                DriverLicenseExpiryDate.Text = lastSavedCourier.driverLicenseExpiryDate;
                DriverLicenseCategory.Text = lastSavedCourier.driverLicenseCategory;
                IsDangerousGoodsAllowed.Text = lastSavedCourier.isAllowedDangerousGoods;
                IsCertified.Text = lastSavedCourier.isCertified;
            }
        }
    }
}