using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using parcelmate.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace parcelmate.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ScannedBarcodesViewModel _scannedBarcodesViewModel;
        [ObservableProperty]
        private bool _showButtons;
        private BarcodeService _barcodeService;

        public MainPageViewModel()
        {
            _scannedBarcodesViewModel = DependencyService.Resolve<ScannedBarcodesViewModel>();
            _barcodeService = DependencyService.Resolve<BarcodeService>();
        }
        [RelayCommand]
        private async Task Delivered(object button)
        {

            if (button != null)
            {
                var itemToDeliver = (string)button;
                bool isValidBarcode = _barcodeService.VerifyBarcode(itemToDeliver);
                if (itemToDeliver != null)
                {
                    if (isValidBarcode)
                    {
                        var action = await Application.Current.MainPage.DisplayActionSheet("Are you sure to mark it as delivered?", "No", "Yes");

                        if (action == "Yes")
                        {
                            ScannedBarcodesViewModel.RemoveIfExist(itemToDeliver);
                        }
                    }
                    else if (!isValidBarcode)
                    {
                        await Application.Current.MainPage.DisplayActionSheet("Invalid Barcode", "This barcode is not valid for delivery", "OK");
                    }


                }
            }
        }
        [RelayCommand]
        private async Task Remove(object button)
        {
            if (button != null)
            {
                var itemToRemove = (string)button;
                if (itemToRemove != null)
                {
                    var action = await Application.Current.MainPage.DisplayActionSheet("Remove this barcode?", "No", "Yes");

                    if (action == "Yes")
                    {
                        _scannedBarcodesViewModel.RemoveIfExist(itemToRemove);
                    }
                }
            }
        }
    }
}