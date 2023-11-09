using parcelmate.Constants;
using parcelmate.Services;
using parcelmate.ViewModels;
using parcelmate.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace parcelmate
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<ScannedBarcodesViewModel>();
            DependencyService.Register<AuthenticationService>();
            DependencyService.Register<CourierDataService>();
            DependencyService.Register<BarcodeService>();
            if (Preferences.Get(AppConstants.IsLoggedInKey, false))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }
    }
}
