using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Mobile_project.Resources;

namespace Mobile_project
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool person1Register = false;
        private bool person2Register = false;
        private bool person3Register = false;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Other_Person_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/RandomPerson.xaml", UriKind.Relative));
        }

        private void Person_1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/DrinkOverview.xaml", UriKind.Relative));
        }

        private void Person_2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml", UriKind.Relative));
        }

        private void Person_3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AddPerson.xaml", UriKind.Relative));
        }

        



        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
       
    }

   
}