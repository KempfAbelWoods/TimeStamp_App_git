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
            var data = new Db_Users
            {
                ID = "1",
                Name = "Pimmel",
                Username = "Username",
                Role = "Role",
                Rights = "Rights",
                Password = "Password",
            };
            
            var Error = Db_Users.CreateTable(Paths.sqlite_path);
            if (Error != null)
            {
                DisplayAlert("Error", Error.GetException().Message, "OK");
            }
            
            var err = Rw_Users.Write(new List<Db_Users> { data },Paths.sqlite_path);
            if (err != null)
            {
                DisplayAlert("Error", err.GetException().Message, "OK");
            }
            
            //Spalte mit alten Daten löschen
            var (list, err1) = Rw_Users.ReadwithID("1", Paths.sqlite_path);

            if (err1 != null)
            {
                DisplayAlert("Error", err1.GetException().Message, "OK");
            }
            
            UserEntry.Text = list[0].Name;

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