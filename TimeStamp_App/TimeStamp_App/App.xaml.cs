using System;
using System.Diagnostics;
using TimeStamp_App.Ansichten;
using TimeStamp_App.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Overview());
        }

        protected override void OnStart()
        {
            //hier wird beim Startup der in der Datenbank angemeldete Benutzer automatisch angemeldet
            var (data,err) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err!=null)
            {
                Trace.WriteLine(err.GetException().ToString());
            }
            else if (data.Count == 1)
            {
                var (list,error) =Rw_Users.ReadwithUserName(data[0].Ressource, Paths.sqlite_path);
                if (error!=null)
                {
                    Trace.WriteLine(err.GetException().ToString()); 
                    MainPage = new NavigationPage(new Login());
                }
                else if (list.Count == 1)
                {
                    Config.enteredUser = list[0].Username;
                    Config.enteredPassword = list[0].Password;
                    MainPage = new NavigationPage(new Overview());
                }

            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}