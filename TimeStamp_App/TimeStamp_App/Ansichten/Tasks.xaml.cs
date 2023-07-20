using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStamp_App.DB;
using TimeStamp_App.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App.Ansichten
{
    public partial class Tasks : ContentPage
    {
        ObservableCollection<Db_Tasks> tasks = new ObservableCollection<Db_Tasks>();
        public Tasks()
        {

            InitializeComponent();
            tasks.Clear();
            var (list, err) = Rw_Tasks.Read("",Paths.sqlite_path);
            
            if(err != null)
            {
                DisplayAlert("Error", err.GetException().Message, "OK");
            }
            
            for (int i = 0; i < list.Count; i++)
            {
                
                tasks.Add(new Db_Tasks { ID = list[i].ID, Description = list[i].Description, 
                    ActualHours = list[i].ActualHours, EstimatedHours = list[i].EstimatedHours});
            }
            

            dataGrid.ItemsSource = tasks;
        }

        private async void GoBackToOverview_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}