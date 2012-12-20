using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;

namespace Mobile_project
{
    public partial class ResultPage : PhoneApplicationPage
    {
        private double legalDrivingLimit;
        private string currentBloodAlc;
        private string personSex;
        private DateTime hoursTilDrivingLimit;
        

        public ResultPage()
        {
            InitializeComponent();

            currentBloodAlc = string.Format("0.00", currentBloodAlc);   //set the proper format for the alcohol level
            hoursTilDrivingLimit = new DateTime(0);
            legalDrivingLimit = 0.2; //the legal driving limit, this will have to be changed to reflect different countries by localization
        }

        private void ResultMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            int hours = 0;
            double bloodAlcoholLevel;
            NavigationContext.QueryString.TryGetValue("bloodAlc", out currentBloodAlc);
            currentBloodAlc = currentBloodAlc.Substring(0, 4);
            
            ResultAlcoholLevelDisplay.Text = currentBloodAlc;
            bloodAlcoholLevel = Convert.ToDouble(currentBloodAlc);
            
            while (bloodAlcoholLevel >= legalDrivingLimit)
            {
                bloodAlcoholLevel = bloodAlcoholLevel - 0.15;
                hours++;
            }

            hoursTilDrivingLimit = DateTime.Now;
            hoursTilDrivingLimit = hoursTilDrivingLimit.AddHours(hours);    //gets the time when one is under the legal driving limit
            ResultDisplayTime.Text = Convert.ToString(hoursTilDrivingLimit);
        }

        private void ResultNotification_Click(object sender, RoutedEventArgs e) //sends a notification to the user when the user can drive again
        {
            Alarm drivingAlarm = new Alarm("Sober notification");
            drivingAlarm.Content = "You are now able to drive. Drive carefully";
            drivingAlarm.BeginTime = hoursTilDrivingLimit;

            ScheduledActionService.Add(drivingAlarm);
        }
    }
}