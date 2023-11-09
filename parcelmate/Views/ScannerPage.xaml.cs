using parcelmate.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace parcelmate.Views
{
    public partial class ScannerPage : ContentPage
    {
        public event EventHandler<string> OnScanned;
        private ScannedBarcodesViewModel _scannedBarcodesViewModel;
        public ScannerPage()
        {
            InitializeComponent();
            _scannedBarcodesViewModel = DependencyService.Resolve<ScannedBarcodesViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartScanning();
        }

        private void StartScanning()
        {
            scannerView.IsScanning = true;
            scannerView.OnScanResult += (result) =>
            {
                if (scannerView.IsScanning)
                {
                    scannerView.AutoFocus();
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (result != null)
                    {
                        await DisplayAlert("Scan Value", result.Text, "OK");
                        scannerView.IsScanning = false;
                        await Navigation.PopAsync();
                        _scannedBarcodesViewModel.AddNew(result.Text);
                    }
                });
            };


        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            scannerView.IsScanning = false;
            await Navigation.PopAsync();
        }
    }
}
