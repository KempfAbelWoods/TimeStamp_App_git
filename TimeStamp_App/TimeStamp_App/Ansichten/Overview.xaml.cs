using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App.Ansichten
{
    public partial class Overview : ContentPage
    {
        public Overview()
        {
            InitializeComponent();
        }

        private async void GoBackToLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void Stamp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimeStamp());
        }

        private async void Orders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Tasks());
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        private async void Connected_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataTransfer());
        }
    }
}