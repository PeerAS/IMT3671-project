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
        private int tempPersonID;


        public AddPerson()
        {
            
            InitializeComponent();

            this.DataContext = App._appViewModel; //sets the datacontext for the database calls

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("mode", out mode);
            NavigationContext.QueryString.TryGetValue("id", out tempID);

            tempPersonID = Convert.ToInt32(tempID);

            if (mode == "Edit") //if we are in edit mode
            {
                tempPersonID = Convert.ToInt32(tempID);
                var personname = from personTable in App._appViewModel.person
                                 where personTable.personID == tempPersonID
                                 select new { name = personTable.personName, weight = personTable.personWeight, sex = personTable.personSex };

                foreach (var item in personname)
                {
                    RegisterNameInput.Text = item.name;
                    RegisterWeightInput.Text = Convert.ToString(item.weight);
                    sex = item.sex;
                }                

                RadioFemale.Visibility = System.Windows.Visibility.Collapsed;   //removes the radio buttons from the screen
                RadioMale.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                this.RegisterWeightInput.Text = "";
                this.RegisterNameInput.Text = "";
            }
            
            base.OnNavigatedTo(e);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e) //when the register button is clicked
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
                if (mode == "Edit") //this is where the database update function will be used
                {
                    weight = Convert.ToDouble(this.RegisterWeightInput.Text);
                    name = Convert.ToString(this.RegisterNameInput.Text);

                    var personname = from personTable in App._appViewModel.person
                                     where personTable.personID == tempPersonID
                                     select personTable;

                    App._appViewModel.UpdatePerson(tempPersonID, name, weight);
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
                        personSex = sex,
                        personID = tempPersonID

                    };

                    App._appViewModel.AddPerson(newPerson); //adds a new person to the database
                }
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App._appViewModel.SaveChangesToDB();    //saves the changes to the database when we leave the page
            base.OnNavigatedFrom(e);
        }      
    }
}