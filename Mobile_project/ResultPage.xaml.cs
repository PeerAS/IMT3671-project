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
        public ResultPage()
        {
            InitializeComponent();
        }

        private void ResultMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string currentBloodAlc;
            NavigationContext.QueryString.TryGetValue("bloodAlc", out currentBloodAlc);

            ResultAlcoholLevelDisplay.Text = currentBloodAlc;
        }
    }
}