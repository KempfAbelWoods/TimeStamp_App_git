using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.Connection;
using Xamarin.Forms;

namespace TimeStamp_App.Ansichten
{
    public partial class Login : ContentPage
    {
        public string enteredUser { get; set; }
        public string enteredPassword { get; set; }
        public Login()
        {
            InitializeComponent();
            enteredUser = UserEntry.Text;
            enteredPassword = PasswordEntry.Text;
            Client client = new Client(enteredUser, enteredPassword);
        }
        

        private async void Anmelden_OnClicked(object sender, EventArgs e)
        {
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