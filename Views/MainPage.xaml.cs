using parcelmate.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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
                var itemToRemove = button.CommandParameter as string;
                if (itemToRemove != null)
                {
                    var action = await DisplayActionSheet("Are you sure to mark it as delivered?", "No", "Yes");

                    if (action == "Yes")
                    {
                        RemoveScannedBarcode(itemToRemove);
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