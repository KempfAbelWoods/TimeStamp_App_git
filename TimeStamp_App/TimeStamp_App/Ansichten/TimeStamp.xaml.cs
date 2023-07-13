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
            
            var (list, err) = Rw_Tasks.Read("",Paths.sqlite_path);
            if(err != null)
            {
                DisplayAlert("Error", err.GetException().Message, "OK");
            }
            Picker.ItemsSource = list.Select(x => x.Description).ToList();
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
                var (list, err) = Rw_Tasks.ReadwithDescription(Picker.SelectedItem.ToString(),Paths.sqlite_path);
                if(err != null)
                {
                    DisplayAlert("Error", err.GetException().Message, "OK");
                }
                string ID = list.Select(x => x.ID).ToString();
                string orderId = list.Select(x => x.orderID).ToString();
                string Description = list.Select(x => x.Description).ToString();
                string UserName = list.Select(x => x.Username).ToString();
                string EstimatedString = list.Select(x => x.EstimatedHours).ToString();
                float Estimated = float.Parse(EstimatedString);
                string ActualString = list.Select(x => x.ActualHours).ToString() + elapsedTime.ToString();
                float ActualHours = float.Parse(ActualString) + (float)elapsedTime.TotalHours;
                string CostsString = list.Select(x => x.Costs).ToString();
                float Costs = float.Parse(CostsString);
                
                var data = new Db_Tasks()
                {
                    ID = ID,
                    orderID = orderId,
                    Description = Description,
                    Username = UserName,
                    EstimatedHours = Estimated,
                    ActualHours = ActualHours,
                    Costs = Costs,
                };
                Rw_Tasks.Write(new List<Db_Tasks> {data}, Paths.sqlite_path);

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
    }
}