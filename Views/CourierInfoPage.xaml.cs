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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace parcelmate.Views
{
    public partial class CourierInfoPage : ContentPage
    {
        public CourierInfoPage()
        {
            InitializeComponent();
        }

        MockDataStore _mockDataStore = new MockDataStore();

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (username == "admin" && password == "admin")
               
            {
                DisplayAlert("Login", "Login successful", "OK");
                LoginFields.IsVisible = false;
                CourierDetails.IsVisible = true;
            }
            else
            {
                DisplayAlert("Error", "Username and password are required", "OK");
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