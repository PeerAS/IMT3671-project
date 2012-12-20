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
        private string randomPersonSex;
        private string weightString;     

        private DateTime currentTime;

        
        public DrinkingTime()
        {
            InitializeComponent();
        }

        private void DrinkingTimeContinue_Click(object sender, RoutedEventArgs e)
        {
            string temp_difference;
            int timeCurrentHours;
            int timeStartedHours;
            int timeDifference;
            

            time_hours = DrinkingTimeHours.Text.ToString(); //gets the user inputed time, minutes are not used atm
            time_minutes = DrinkingTimeMinutes.Text.ToString(); 
            total_time = time_hours + ":" + time_minutes;

            
            timeStartedHours = Convert.ToInt32(time_hours);
            currentTime = DateTime.Now;
            timeCurrentHours = currentTime.Hour;

            timeDifference = timeCurrentHours - timeStartedHours;   //gets the time difference between now and when the drinking started
                                                                    //might add an option for half hours as well
            temp_difference = Convert.ToString(timeDifference);

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

        private void DrinkingTimeHours_GotFocus(object sender, RoutedEventArgs e)   //empties the inputs when you tap them so you don't manually have to remove the 00
        {
            DrinkingTimeHours.Text = "";
        }

        private void DrinkingTimeMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            DrinkingTimeMinutes.Text = "";
        }

        private void DrinkingTimeMinutes_LostFocus(object sender, RoutedEventArgs e)
        {
            double tempMinutes = Convert.ToDouble(DrinkingTimeMinutes.Text.ToString());
            if (DrinkingTimeMinutes.Text == "")
                DrinkingTimeMinutes.Text = "00";
            else if (tempMinutes >= 60)
                DrinkingTimeMinutes.Text = "59";                
        }

        private void DrinkingTimeHours_LostFocus(object sender, RoutedEventArgs e)
        {
            double tempHours = Convert.ToDouble(DrinkingTimeHours.Text.ToString());

            if (DrinkingTimeHours.Text == "")
                DrinkingTimeHours.Text = "00";
            else if (tempHours >= 24)
                DrinkingTimeHours.Text = "23";
        }

    }
}