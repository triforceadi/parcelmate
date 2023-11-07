using parcelmate.ViewModels;
using parcelmate.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace parcelmate
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(CourierInfoPage), typeof(CourierInfoPage));
            Routing.RegisterRoute(nameof(AvailableDeliveriesPage), typeof(AvailableDeliveriesPage));
        }
    }
}
