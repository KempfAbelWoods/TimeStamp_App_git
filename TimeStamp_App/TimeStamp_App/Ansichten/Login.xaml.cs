using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.Connection;
using TimeStamp_App.DB;
using Xamarin.Forms;

namespace TimeStamp_App.Ansichten
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            
            var Error = Db_Users.CreateTable(Paths.sqlite_path);
            if (Error != null)
            {
                DisplayAlert("Error", Error.GetException().Message, "OK");
            }

        }


        private async void Anmelden_OnClicked(object sender, EventArgs e)
        {
            Config.enteredUser = UserEntry.Text;
            Config.enteredPassword = PasswordEntry.Text;
            Client.SocketClient();
            Textfeld.Text = Client.Ausgabe;
            await Navigation.PushAsync(new Overview());
        }

        void Abbrechen_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}