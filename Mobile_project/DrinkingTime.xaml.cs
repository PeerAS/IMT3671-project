using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Mobile_project
{
    public partial class DrinkingTime : PhoneApplicationPage
    {
        
        private double weight;
        private int personID;

        private string time_hours;
        private string time_minutes;
        private string total_time;
        private string currentTimeString;
        private string randomPersonSex;
        private string weightString;

        private DateTime drinkingStarted;     
        private TimeSpan drinkingTime;
        private DateTime currentTime;

        
        public DrinkingTime()
        {
            InitializeComponent();
        }

        private void DrinkingTimeContinue_Click(object sender, RoutedEventArgs e)
        {
            string temp_difference;

            time_hours = DrinkingTimeHours.Text.ToString();
            time_minutes = DrinkingTimeMinutes.Text.ToString();
            total_time = time_hours + ":" + time_minutes;


            //drinkingStarted = Convert.ToDateTime(total_time);
            //currentTime = DateTime.Now;

            //drinkingTime = currentTime.Subtract(drinkingStarted);
            temp_difference = time_hours;

            if (randomPersonSex == null)
                this.NavigationService.Navigate(new Uri("/DrinkOverview.xaml?time=" + temp_difference + "&id=" + personID, UriKind.Relative));
            else
                this.NavigationService.Navigate(new Uri("/DrinkOverview.xaml?time=" + temp_difference + "&weight=" + weightString + "&sex=" + randomPersonSex, UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            string personIDString;
            randomPersonSex = null;

            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("weight", out weightString);
            NavigationContext.QueryString.TryGetValue("id", out personIDString);
            NavigationContext.QueryString.TryGetValue("sex", out randomPersonSex);

            weight = Convert.ToDouble(weightString);
            personID = Convert.ToInt32(personIDString);
        }

        private void DrinkingTimeHours_GotFocus(object sender, RoutedEventArgs e)
        {
            DrinkingTimeHours.Text = "";
        }

        private void DrinkingTimeMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            DrinkingTimeMinutes.Text = "";
        }

    }
}