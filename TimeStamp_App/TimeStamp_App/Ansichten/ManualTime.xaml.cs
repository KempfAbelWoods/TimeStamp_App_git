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
public partial class ManualTime : ContentPage
{
    public ManualTime()
    {
        InitializeComponent();
        
        var (list, err) = Rw_Tasks.Read("",Paths.sqlite_path);
        if(err != null)
        {
            DisplayAlert("Error", err.GetException().Message, "OK");
        }
        Picker.ItemsSource = list.Select(x => x.ID).ToList();
    }

    private void AddTime(object sender, EventArgs e)
    {
        string Input = EnteredTime.Text;
        
        if (float.TryParse(Input, out float floatValue))
        {
            // Zeit in DB schreiben
            var (list, err) = Rw_Tasks.ReadwithID(Picker.SelectedItem.ToString(),Paths.sqlite_path);
            if(err != null)
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
            float Actual = (float)list[0].ActualHours + floatValue;
            float ActualRounded = (float)Math.Round(Actual, 2);
            float Costs = (float)list[0].Costs;
            float CostsRounded = (float)Math.Round(Costs, 2);
                
                
            var data = new Db_Tasks()
            {
                ID = ID,
                OrderId =  orderId,
                Description = Description,
                Ressource = Ressource,
                Username = UserName,
                EstimatedHours = EstimatedRounded,
                ActualHours = ActualRounded,
                Costs = CostsRounded,
            };
            Rw_Tasks.Write(new List<Db_Tasks> {data}, Paths.sqlite_path);
        }
        else
        {
            DisplayAlert("Error", "Bitte gültige Zeit eingeben", "OK");
        }
        
        
    }

    private async void GoBackToOverview_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

public class FloatEntry : Entry
{
    public static readonly BindableProperty FloatValueProperty =
        BindableProperty.Create(nameof(FloatValue), typeof(float), typeof(FloatEntry), default(float));

    public float FloatValue
    {
        get { return (float)GetValue(FloatValueProperty); }
        set { SetValue(FloatValueProperty, value); }
    }

    public FloatEntry()
    {
        this.TextChanged += OnTextChanged;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (float.TryParse(e.NewTextValue, out float floatValue))
            {
                FloatValue = floatValue;
            }
            else
            {
                
            }
        }
    }
}