using parcelmate.Constants;
using parcelmate.Services;
using parcelmate.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;


namespace parcelmate.Views
{
    public partial class MainPage : ContentPage
    {
        private BarcodeService barcodeService;
        public MainPage()
        {
            InitializeComponent();
            barcodeService = new BarcodeService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                NotLoggedInParcelsText.IsVisible = false;
                scannedParcelsText.IsVisible = true;
            }
            else
            {
                NotLoggedInParcelsText.IsVisible = true;
                scannedParcelsText.IsVisible = false;
            }
        }

        private async void OnOpenScannerButtonClicked(object sender, EventArgs e)
        {
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                var scannerPage = new ScannerPage();

                scannerPage.OnScanned += (s, result) =>
                {
                    DisplayAlert("Scanned", $"Barcode: {result}", "OK");
                    Vibration.Vibrate();
                };
                await Navigation.PushAsync(scannerPage);
            }
            else
            {
                await DisplayAlert("Currently logged out", "You must be logged in to scan a parcel", "OK");
            }
        }

    }
}