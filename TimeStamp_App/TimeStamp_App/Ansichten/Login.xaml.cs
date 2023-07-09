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
        public bool LoggedIn;
        public Login()
        {
            InitializeComponent();
        }

        private async void Log_in(object sender, EventArgs e)
        {
        var (list, err) = Rw_Users.ReadwithUserName(UserEntry.Text, Paths.sqlite_path);
        if (err != null)
        {
            DisplayAlert("Error", err.GetException().Message, "OK");
        }
        else if (list.Count == 0)
        {
            DisplayAlert("Error", "User does not exist", "OK");
        }
        
        Config.enteredUser = UserEntry.Text;
        Config.enteredPassword = PasswordEntry.Text;
        
        if (list.Count == 1)
        {
            if (PasswordEntry.Text == list[0].Password)
            {
                if (StayLogged.IsChecked)
                {
                    var data = new Db_Settings
                    {
                        ID = "1",
                        Name = "LoggedUser",
                        Ressource = list[0].Username,
                        Comment = "Hier steht der aktuell angemeldete Benutzer, wenn niemand angemeldet ist ist das Feld leer"
                    };
                    //alte Zeile löschen
                    var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
                    if (err2 != null)
                    {
                        DisplayAlert("Error", err2.GetException().Message, "OK");
                    }
                    if (Row.Count == 1)
                    {
                        var error = Rw_Settings.Delete(Row, Paths.sqlite_path);
                        if (error != null)
                        {
                            DisplayAlert("Error", error.GetException().Message, "OK");
                        }
                    }
                    //neue Zeile einfügen
                    var err1 = Rw_Settings.Write(new List<Db_Settings>{data}, Paths.sqlite_path);
                    if (err1 != null)
                    {
                       DisplayAlert("Error", err.GetException().Message, "OK");
                    }
                    Client.SocketClientUser("");
                }
                else
                {
                    var data = new Db_Settings
                    {
                        ID = "1",
                        Name = "LoggedUser",
                        Ressource = "",
                        Comment = "Hier steht der aktuell angemeldete Benutzer, wenn niemand angemeldet ist ist das Feld leer"
                    };
                    //alte Zeile löschen
                    var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
                    if (err2 != null)
                    {
                        DisplayAlert("Error", err2.GetException().Message, "OK");
                    }
                    if (Row.Count == 1)
                    {
                        var error = Rw_Settings.Delete(Row, Paths.sqlite_path);
                        if (error != null)
                        {
                            DisplayAlert("Error", error.GetException().Message, "OK");
                        }
                    }
                    //neue Zeile einfügen
                    var err1 = Rw_Settings.Write(new List<Db_Settings>{data}, Paths.sqlite_path);
                    if (err1 != null)
                    {
                        DisplayAlert("Error", err.GetException().Message, "OK");
                    }

                    LoggedIn = true;
                    Client.SocketClientUser("");
                }
                await Navigation.PushAsync(new Overview());
                
            }
            else
            {
                DisplayAlert("Error", "Wrong Password", "OK");
            }
        }
    }

         void Abbrechen_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}