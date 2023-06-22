﻿using System;
using TimeStamp_App.Ansichten;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeStamp_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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