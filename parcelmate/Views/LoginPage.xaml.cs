using parcelmate.Constants;
using parcelmate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace parcelmate.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<LoginViewModel>(this, "LoginSuccess", async (sender) =>
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            });
        }
    }
}