using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;


namespace parcelmate.Views
{
    public partial class MainPage : ContentPage
    {
       readonly ObservableCollection<string> scanResults = new ObservableCollection<string>();
        public MainPage()
        {
            InitializeComponent();
            scanResultsListView.ItemsSource = scanResults;

            MessagingCenter.Subscribe<ScannerPage, string>(this, "ScanResult", (sender, result) =>
            {
                scanResults.Add(result);
            });

            BindingContext = this;
        }

        private async void OnOpenScannerButtonClicked(object sender, EventArgs e)
        {
            var scannerPage = new ScannerPage();

            scannerPage.OnScanned += (s, result) =>
            {
                // Handle the scanned result in the main page
                DisplayAlert("Scanned", $"Barcode: {result}", "OK");
                scanResults.Insert(0, result);
            };

            // Navigate to the ScannerPage
            await Navigation.PushAsync(scannerPage);
        }

        private async void OnTappedScannedBarcode(object sender, EventArgs e)
        {
            if (e is ItemTappedEventArgs eventArgs)
            {
                if (eventArgs.Item is string barcode)
                {
                    var action = await DisplayActionSheet("Mark as delivered?", "Cancel", "Yes");

                    if (action == "Yes")
                    {
                        RemoveScannedBarcode(barcode);
                    }
                    
                }
            }
        }

        private void RemoveScannedBarcode(string barcode)
        {
            // Remove the barcode from your collection
            if (scanResults.Contains(barcode))
            {
                scanResults.Remove(barcode);
            }
        }
    }
}