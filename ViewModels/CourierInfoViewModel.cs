using parcelmate.Models;
using parcelmate.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace parcelmate.ViewModels
{
    public class CourierInfoViewModel : BaseViewModel
    {
        private const string IsLoggedInKey = "IsLoggedIn";
        private bool isLoggedIn;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set
            {
                if (isLoggedIn != value)
                {
                    isLoggedIn = value;
                    OnPropertyChanged(nameof(IsLoggedIn));
                }
            }
        }

        public CourierInfoViewModel()
        {
            IsLoggedIn = Preferences.Get(IsLoggedInKey, true);
        }

        public void SignOut()
        {
            Preferences.Set(IsLoggedInKey, false);
            IsLoggedIn = false;
        }
    }
}