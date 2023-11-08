using parcelmate.Constants;
using parcelmate.Services;
using parcelmate.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace parcelmate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvailableDeliveriesPage : ContentPage
    {
        private BarcodeService barcodeService;
        readonly ObservableCollection<string> barcodesAvailable = new ObservableCollection<string>();

        private AvailableDeliveriesViewModel viewModel;
        public AvailableDeliveriesPage()
        {
            InitializeComponent();
            barcodeService = DependencyService.Resolve<BarcodeService>();
            barcodesAvailableListView.ItemsSource = barcodeService.AvailableBarcodes();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Preferences.Get(AppConstants.IsLoggedInKey, true))
            {
                barcodesAvailableListView.IsVisible = true;
                NotLoggedInDisplayText.IsVisible = false;
            }
            else
            {
                barcodesAvailableListView.IsVisible = false;
                NotLoggedInDisplayText.IsVisible = true;
            }
        }
    }
}