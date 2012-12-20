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
        private int arrayIterator;
        private int personID;
        private string mode;
        private string emptyUser;

        // Constructor
        public MainPage()
        {
            
            InitializeComponent();
            names = new string[MAX_USERS]{"Register New Person", "Register New Person", "Register New Person"};
            emptyUser = "There is no registered person here.";

            arrayIterator = 0;
            this.DataContext = App._appViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            arrayIterator = 0;

            if (App._appViewModel.person.Count() != 0)
            {
                var personname = from personTable in App._appViewModel.person   //select from the observable collection person
                                 where personTable.personID <= MAX_USERS        //and put it in a var.
                                 select new { Name = personTable.personName, Id = personTable.personID };


                foreach (var item in personname)
                {
                    names[item.Id] = item.Name;
                }
            }

            Person_1.Content = names[0];
            Person_2.Content = names[1];
            Person_3.Content = names[2];


            base.OnNavigatedTo(e);
        }

        private void Other_Person_Click(object sender, RoutedEventArgs e)
        {
            if (mode == "Edit")
            {
                this.Random_Person.Content = "Other Person";
                mode = null;
                ModeText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.NavigationService.Navigate(new Uri("/RandomPerson.xaml", UriKind.Relative));
            }
        }

        private void Person_1_Click(object sender, RoutedEventArgs e)
        {
            personID = 0;
            var person1 = from personTable in App._appViewModel.person
                          where personTable.personID == personID
                          select new { Id = personTable.personID };
            
            if ((mode == "Edit" || mode == "Delete")  && person1.Count() == 0)
            {                
                MessageBox.Show(emptyUser);
            }
            else if (mode == "Edit")
            {                
                this.NavigationService.Navigate(new Uri("/AddPerson.xaml?mode="+mode+"&id="+personID, UriKind.Relative));
            }
            else if (mode == "Delete")
            {
                App._appViewModel.DeletePerson(personID);
                Person_1.Content = "Register Person";
                Random_Person.Content = "Other Person";
                ModeText.Visibility = System.Windows.Visibility.Collapsed;
                mode = "";
            }
            else if (person1.Count() == 1)
            {
                this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml?id=" + personID, UriKind.Relative));
            }
            else if (person1.Count() == 0)
            {
                this.NavigationService.Navigate(new Uri("/Addperson.xaml?id=" + personID, UriKind.Relative));
            }
           
        }

        private void Person_2_Click(object sender, RoutedEventArgs e)
        {
            personID = 1;
            var person2 = from personTable in App._appViewModel.person
                          where personTable.personID == personID
                          select new { Id = personTable.personID};
           
            if (mode == "Edit" && person2.Count() == 0)
            {
                MessageBox.Show(emptyUser);
            }
            else if (mode == "Edit")
            {
                this.NavigationService.Navigate(new Uri("/AddPerson.xaml?mode=" + mode + "&id=" + personID, UriKind.Relative));
            }
            else if (mode == "Delete")
            {
                App._appViewModel.DeletePerson(personID);
                Person_2.Content = "Register Person";
                Random_Person.Content = "Other Person";
                ModeText.Visibility = System.Windows.Visibility.Collapsed;
                mode = "";
            }
            else if (person2.Count() == 0)
            {
                this.NavigationService.Navigate(new Uri("/AddPerson.xaml?id=" + personID, UriKind.Relative));
            }
            else if (person2.Count() == 1)
            {
                this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml?id=" + personID, UriKind.Relative));
            }

        }

        private void Person_3_Click(object sender, RoutedEventArgs e)
        {
            personID = 2;
            var person3 = from personTable in App._appViewModel.person
                          where personTable.personID == personID
                          select new { Id = personTable.personID };

            if (mode == "Edit" && person3.Count() == 0)
            {
                MessageBox.Show(emptyUser);
            }
            else if (mode == "Edit")
            {
                this.NavigationService.Navigate(new Uri("/AddPerson.xaml?mode=" + mode + "&id=" + personID, UriKind.Relative));
            }
            else if (mode == "Delete")
            {
                App._appViewModel.DeletePerson(personID);
                Person_3.Content = "Register Person";
                Random_Person.Content = "Other Person";
                ModeText.Visibility = System.Windows.Visibility.Collapsed;
                mode = "";
            }
            else if (person3.Count() == 0)
            {
                this.NavigationService.Navigate(new Uri("/AddPerson.xaml?id=" + personID, UriKind.Relative));
            }
            else if (person3.Count() == 1)
            {
                this.NavigationService.Navigate(new Uri("/DrinkingTime.xaml?id=" + personID, UriKind.Relative));
            }
        }

        private void edit_delete_person_OnClick(object sender, EventArgs e)
        {
            ModeText.Visibility = System.Windows.Visibility.Visible;
            ModeText.Text = "Select person to edit";
            Random_Person.Content = "Cancel";
            mode = "Edit";
        }

        private void DeletePerson_OnClick(object sender, EventArgs e)
        {
            ModeText.Visibility = System.Windows.Visibility.Visible;
            ModeText.Text = "Select person to delete";
            Random_Person.Content = "Cancel";
            mode = "Delete";
        }

        //// Sample code for building a localized ApplicationBar
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