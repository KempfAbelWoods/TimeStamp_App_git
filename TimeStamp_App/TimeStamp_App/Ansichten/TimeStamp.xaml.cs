using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App.Ansichten
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeStamp : ContentPage
    {
        private bool isTimerRunning;
        private DateTime startTime;
        private TimeSpan elapsedTime;
        public List<Db_Tasks> Tasks { get; set; }

        public TimeStamp()
        {
            InitializeComponent();

            var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err2 != null)
            {
                DisplayAlert("Error", err2.GetException().Message, "OK");
            }

            var (list, err) = Rw_Tasks.Read("", Paths.sqlite_path);
            if (err != null)
            {
                DisplayAlert("Error", err.GetException().Message, "OK");
            }
            
            Picker.ItemsSource = list.Where(x => x.Username == Row[0].Ressource).Select(x => x.ID).ToList();

        }

        private async void GoBackToOverview_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void TimeStart(object sender, EventArgs e)
        {
            if (!isTimerRunning)
            {
                isTimerRunning = true;
                startTime = DateTime.Now;
                Device.StartTimer(TimeSpan.FromSeconds(0.1), UpdateTimer);
            }
            else
            {
                DisplayAlert("Error", "Zeit läuft bereits!", "OK");
            }
        }

        private void TimeStop(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                isTimerRunning = false;
                elapsedTime = DateTime.Now - startTime;
                // Zeit in DB schreiben
                var (list, err) = Rw_Tasks.ReadwithID(Picker.SelectedItem.ToString(), Paths.sqlite_path);
                if (err != null)
                {
                    DisplayAlert("Error", err.GetException().Message, "OK");
                }

                string ID = (string)list[0].ID.ToString();
                string orderId = (string)list[0].OrderId.ToString();
                string Description = (string)list[0].Description.ToString();
                string Ressource = (string)list[0].Ressource.ToString();
                string UserName = (string)list[0].Username.ToString();
                float Estimated = (float)list[0].EstimatedHours;
                float EstimatedRounded = (float)Math.Round(Estimated, 2);
                float Actual = (float)list[0].ActualHours + (float)elapsedTime.TotalHours;
                float ActualRounded = (float)Math.Round(Actual, 2);
                float Costs = (float)list[0].Costs;
                float CostsRounded = (float)Math.Round(Costs, 2);


                var data = new Db_Tasks()
                {
                    ID = ID,
                    OrderId = orderId,
                    Description = Description,
                    Ressource = Ressource,
                    Username = UserName,
                    EstimatedHours = EstimatedRounded,
                    ActualHours = ActualRounded,
                    Costs = CostsRounded,
                };
                Rw_Tasks.Write(new List<Db_Tasks> { data }, Paths.sqlite_path);
            }
        }

        private bool UpdateTimer()
        {
            if (isTimerRunning)
            {
                elapsedTime = DateTime.Now - startTime;
                return true; // Timer weiterführen
            }
            else
            {
                return false; // Timer beenden
            }
        }

        private async void ManualTime(object sender, EventArgs e)
        {
            var (Row, err2) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
            if (err2 != null)
            {
                DisplayAlert("Error", err2.GetException().Message, "OK");
            }

            if (Row[0].Ressource == "admin")
            {
                await Navigation.PushAsync(new ManualTime());
            }
            else if (err2 == null)
            {
                DisplayAlert("Error", "Nicht die benötigten Rechte!", "OK");
            }
            else
            {
                DisplayAlert("Error", "Nicht die benötigten Rechte!", "OK");
            }
        }
    }
}