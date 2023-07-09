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
        public TimeStamp()
        {
            InitializeComponent();
            
            var (list, err) = Rw_Tasks.Read("",Paths.sqlite_path);
            if(err != null)
            {
                DisplayAlert("Error", err.GetException().Message, "OK");
            }
            Picker.ItemsSource = list.Select(x => x.ID).ToList();
        }
        
        private async void GoBackToOverview_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void TimeStart(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TimeStop(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}