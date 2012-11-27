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
using Mobile_project.ViewModel;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mobile_project
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string[] names;
        private double[] weights;
        const int MAX_USERS = 3;
        private int i;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            names = new string[MAX_USERS]{"Register New Person", "Register New Person", "Register New Person"};
            weights = new double[MAX_USERS];
            i = 0;
            this.DataContext = App._appViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            if (App._appViewModel.person.Count() != 0)
            {
                var personname = from personTable in App._appViewModel.person
                                 where personTable.personID <= MAX_USERS
                                 select new { Name = personTable.personName, weight = personTable.personWeight };

                
                foreach (var item in personname)
                {
                    names[i] = item.Name;
                    weights[i] = item.weight;
                    i++;
                }

                Person_1.Content = names[0];
                Person_2.Content = names[1];
                Person_3.Content = names[2];
            }
        }

        private void Other_Person_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/RandomPerson.xaml", UriKind.Relative));
        }

        private void Person_1_Click(object sender, RoutedEventArgs e)
        {
            double temp_weight = weights[1];
            this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml?weight="+temp_weight, UriKind.Relative));
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