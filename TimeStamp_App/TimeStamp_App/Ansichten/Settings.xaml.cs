using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App.Ansichten;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
    }

    private void Create_Table_Users(object sender, EventArgs e)
    {
        var Error = Db_Users.CreateTable(Paths.sqlite_path);
        if (Error != null)
        {
            DisplayAlert("Error", Error.GetException().Message, "OK");
        }
    }

    private void Create_Table_Settings(object sender, EventArgs e)
    {
        var Error = Db_Settings.CreateTable(Paths.sqlite_path);
        if (Error != null)
        {
            DisplayAlert("Error", Error.GetException().Message, "OK");
        }
    }
    
    private void Create_Table_Tasks(object sender, EventArgs e)
    {
        var Error = Db_Tasks.CreateTable(Paths.sqlite_path);
        if (Error != null)
        {
            DisplayAlert("Error", Error.GetException().Message, "OK");
        }
    }
}