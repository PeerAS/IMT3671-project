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
        private string sex;
        private string mode;
        private string tempID;
        private double weight;
        private int personID;


        public AddPerson()
        {
            
            InitializeComponent();

            this.DataContext = App._appViewModel;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("mode", out mode);
            NavigationContext.QueryString.TryGetValue("id", out tempID);

            if (mode == "Edit")
            {
                personID = Convert.ToInt16(tempID);
                var personname = from personTable in App._appViewModel.person
                                 where personTable.personID == personID
                                 select new { name = personTable.personName, weight = personTable.personWeight, sex = personTable.personSex };

                foreach (var item in personname)
                {
                    RegisterNameInput.Text = item.name;
                    RegisterWeightInput.Text = Convert.ToString(item.weight);
                    sex = item.sex;
                }                

                RadioFemale.Visibility = System.Windows.Visibility.Collapsed;
                RadioMale.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                this.RegisterWeightInput.Text = "";
                this.RegisterNameInput.Text = "";
            }
            
            base.OnNavigatedTo(e);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (RadioMale.IsChecked == false && RadioFemale.IsChecked == false && mode != "Edit")
            {
                MessageBox.Show("You have not selected a sex");
            }
            else if (RegisterNameInput.Text == "")
            {
                MessageBox.Show("You have not entered a name");
            }
            else if (RegisterWeightInput.Text == "")
            {
                MessageBox.Show("You have not entered a weight");
            }
            else
            {
                if (mode == "Edit")
                {
                    weight = Convert.ToDouble(this.RegisterWeightInput.Text);

                    var personname = from personTable in App._appViewModel.person
                                     where personTable.personID == personID
                                     select personTable;
                }
                else
                {
                    weight = Convert.ToDouble(this.RegisterWeightInput.Text);
                    if (RadioMale.IsChecked == true)
                        sex = "Male";
                    else if (RadioFemale.IsChecked == true)
                        sex = "Female";

                    PersonData newPerson = new PersonData
                    {
                        personName = this.RegisterNameInput.Text,
                        personWeight = weight,
                        personSex = sex

                    };

                    App._appViewModel.AddPerson(newPerson);
                }
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App._appViewModel.SaveChangesToDB();
            base.OnNavigatedFrom(e);
        }      
    }
}