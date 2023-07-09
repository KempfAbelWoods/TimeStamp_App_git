using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.DB;
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
            var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err2 != null)
            {
                DisplayAlert("Error", err2.GetException().Message, "OK");
            }

            if (Row[0].Ressource != "")
            {
                await Navigation.PushAsync(new TimeStamp());
            }
            else
            {
                DisplayAlert("Error", "Bitte erst anmelden", "OK");
            }
        }

        private async void Orders_Clicked(object sender, EventArgs e)
        {
            var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err2 != null)
            {
                DisplayAlert("Error", err2.GetException().Message, "OK");
            }

            if (Row[0].Ressource != "")
            {
                await Navigation.PushAsync(new Tasks());
            }
            else
            {
                DisplayAlert("Error", "Bitte erst anmelden", "OK");
            }
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err2 != null)
            {
                DisplayAlert("Error", err2.GetException().Message, "OK");
            }

            if (Row[0].Ressource != "")
            {
                await Navigation.PushAsync(new Settings());
            }
            else
            {
                DisplayAlert("Error", "Bitte erst anmelden", "OK");
            }
        }

        private async void Connected_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataTransfer());
        }
    }
}