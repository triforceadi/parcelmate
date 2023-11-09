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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            courierDataService.InitializeData();
            authService.InitializeData();
            PopulateFields();

        }


        private void PopulateFields(object sender, EventArgs e)
        {
            try
            {
                int userId = authService.ReturnUserIdByUsername(AppConstants.Username);
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

        private void PopulateFields()
        {
            try
            {
                int userId = authService.ReturnUserIdByUsername(AppConstants.Username);
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