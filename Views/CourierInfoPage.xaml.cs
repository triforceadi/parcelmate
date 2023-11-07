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
        private const string IsLoggedInKey = "IsLoggedIn";
        public CourierInfoViewModel ViewModel
        {
            get { return (CourierInfoViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public CourierInfoPage()
        {
            InitializeComponent();
            ViewModel = new CourierInfoViewModel();
            if (Preferences.Get(IsLoggedInKey, true))
            {
                LoginFields.IsVisible = false;
                CourierDetails.IsVisible = true;
            }
        }

        MockDataStore _mockDataStore = new MockDataStore();

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (username == "admin" && password == "admin")
               
            {
                Preferences.Set(IsLoggedInKey, true);
                DisplayAlert("Login", "Login successful", "OK");
                LoginFields.IsVisible = false;
                CourierDetails.IsVisible = true;

            }
            else
            {
                DisplayAlert("Error", "Username and password are required", "OK");
            }
        }

        private async void OnSignOutToolbarItemClicked(object sender, EventArgs e)
        {
            if(Preferences.Get(IsLoggedInKey, true))
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
                DisplayAlert("Sign out","You must be logged in first", "OK");
            }

        }

        private void OnSaveClicked(object sender, EventArgs e)
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

            _mockDataStore.AddItemAsync(courier);
            DisplayAlert("Save", "Data saved successfully", "OK");
        }
    }
}