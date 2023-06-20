using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.Connection;
using Xamarin.Forms;

namespace TimeStamp_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //sean dumm
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Client.SocketClient();
            Textfeld.Text = Client.Ausgabe;
        }
    }
}