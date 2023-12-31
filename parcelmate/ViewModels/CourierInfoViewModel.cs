﻿using parcelmate.Constants;
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

        public CourierInfoViewModel()
        {
            IsLoggedIn = Preferences.Get(AppConstants.IsLoggedInKey, true);
        }

        public void SignOut()
        {
            Preferences.Set(AppConstants.IsLoggedInKey, false);
            IsLoggedIn = false;
        }
    }
}