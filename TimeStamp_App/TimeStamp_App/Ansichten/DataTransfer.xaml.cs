using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.Connection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App.Ansichten;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DataTransfer : ContentPage
{
    public DataTransfer()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Config.ConnectionCode = ConnectionCode.Text;
        Client.SocketClientCode(ConnectionCode.Text);
    }
}