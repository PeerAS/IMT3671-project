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
    public partial class ResultPage : PhoneApplicationPage
    {
        private double legalDrivingLimit;
        private string currentBloodAlc;
        private DateTime hoursTilDrivingLimit;
        

        public ResultPage()
        {
            InitializeComponent();

            currentBloodAlc = string.Format("0.00", currentBloodAlc);
            hoursTilDrivingLimit = new DateTime(0);
            legalDrivingLimit = 0.2;
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
            
            while (bloodAlcoholLevel > legalDrivingLimit)
            {
                bloodAlcoholLevel = bloodAlcoholLevel - 0.15;
                hours++;
            }
            hoursTilDrivingLimit = DateTime.Now;
            hoursTilDrivingLimit = hoursTilDrivingLimit.AddHours(hours);
            ResultDisplayTime.Text = Convert.ToString(hoursTilDrivingLimit);
        }
    }
}