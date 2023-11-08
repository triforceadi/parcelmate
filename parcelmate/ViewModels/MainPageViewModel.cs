using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace parcelmate.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<string> _scanResults;
        private bool _showButtons;
        public ObservableCollection<string> ScanResults
        {
            get => _scanResults;
            set
            {
                _scanResults = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ScanResultsVisible));
            }
        }

        public bool ScanResultsVisible => _scanResults.Count > 0;

        public MainPageViewModel()
        {
            _scanResults = new ObservableCollection<string>();
        }
        public bool ShowButtons
        {
            get => _showButtons;
            set
            {
                _showButtons = value;
                OnPropertyChanged();
            }
        }

    }
}