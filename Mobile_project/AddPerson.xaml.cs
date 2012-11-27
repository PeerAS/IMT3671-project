using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;

using Database.Model;
using Mobile_project.ViewModel;

namespace Mobile_project
{
    public partial class AddPerson : PhoneApplicationPage
    {
        private string name;
        private double weight;

        public AddPerson()
        {
            InitializeComponent();

            this.DataContext = App._appViewModel;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            this.RegisterWeightInput.Text = "";
            this.RegisterNameInput.Text = "";
            
            base.OnNavigatedTo(e);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {          
            weight = Convert.ToDouble(this.RegisterWeightInput.Text);
            
            PersonData newPerson = new PersonData
            {
                personName = this.RegisterNameInput.Text,
                personWeight = weight
            };

            App._appViewModel.AddPerson(newPerson);

            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App._appViewModel.SaveChangesToDB();
            base.OnNavigatedFrom(e);
        }      
    }
}