using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace parcelmate.ViewModels
{
    public partial class ScannedBarcodesViewModel :ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> _barcodes = new ObservableCollection<string>();
        
        public void AddNew(string barcode)
        {
            Barcodes.Add(barcode);
        }
        public void RemoveIfExist(string barcode)
        {
            Barcodes.Remove(Barcodes.FirstOrDefault(c => c == barcode));
        }
    }
}
