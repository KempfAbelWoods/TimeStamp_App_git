using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App.Ansichten
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDos : ContentPage
    {
        public ToDos()
        {
            InitializeComponent();
        }
        
        private async void GoBackToOverview_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}