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
    public partial class RandomPerson : PhoneApplicationPage
    {
        private string personSex;
        private string randomWeight;
        public RandomPerson()
        {
            InitializeComponent();
        }

        private void RandomContinue_Click(object sender, RoutedEventArgs e)
        {
            randomWeight = RandomWeightInput.Text;
            if (RadioMale.IsChecked == false && RadioFemale.IsChecked == false || randomWeight == "")
                MessageBox.Show("You have not selected a sex/entered weight");
            else
                this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml?sex="+personSex+"&weight="+randomWeight, UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            personSex = null;
            RadioMale.IsChecked = false;
            RadioFemale.IsChecked = false;
        }

        private void RadioMale_Checked(object sender, RoutedEventArgs e)
        {
            personSex = "Male";
        }

        private void RadioFemale_Checked(object sender, RoutedEventArgs e)
        {
            personSex = "Female";
        }

        
    }
}