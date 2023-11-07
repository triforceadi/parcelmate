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
       readonly ObservableCollection<string> scanResults = new ObservableCollection<string>();
        private BarcodeService barcodeService;
        public MainPage()
        {
            InitializeComponent();
            barcodeService = new BarcodeService();
            scanResultsListView.ItemsSource = scanResults;
            

            MessagingCenter.Subscribe<ScannerPage, string>(this, "ScanResult", (sender, result) =>
            {
                scanResults.Add(result);
            });

            BindingContext = this;
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
                    scanResults.Insert(0, result);
                };
                await Navigation.PushAsync(scannerPage);
            }
            else
            {
                await DisplayAlert("Currently logged out", "You must be logged in to scan a parcel", "OK");
            }
        }

        private void RemoveScannedBarcode(string barcode)
        {
            if (scanResults.Contains(barcode))
            {
                scanResults.Remove(barcode);
            }
        }
        private async void OnDeliveredButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var itemToDeliver = button.CommandParameter as string;
                bool isValidBarcode = barcodeService.VerifyBarcode(itemToDeliver);
                if (itemToDeliver != null)
                {
                    if (isValidBarcode)
                    {
                        var action = await DisplayActionSheet("Are you sure to mark it as delivered?", "No", "Yes");

                        if (action == "Yes")
                        {
                            RemoveScannedBarcode(itemToDeliver);
                        }
                    }
                    else if (!isValidBarcode)
                    {
                        await DisplayAlert("Invalid Barcode", "This barcode is not valid for delivery", "OK");
                    }


                }
            }
        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var itemToRemove = button.CommandParameter as string;
                if (itemToRemove != null)
                {
                    var action = await DisplayActionSheet("Remove this barcode?", "No", "Yes");

                    if (action == "Yes")
                    {
                        RemoveScannedBarcode(itemToRemove);
                    }
                }
            }
        }


    }
}