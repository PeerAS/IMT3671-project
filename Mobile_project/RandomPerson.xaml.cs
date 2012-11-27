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
        public RandomPerson()
        {
            InitializeComponent();
        }

        private void RandomContinue_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml", UriKind.Relative));
        }
    }
}