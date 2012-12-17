using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Mobile_project.ViewModel;

namespace Mobile_project
{
    public partial class DrinkOverview : PhoneApplicationPage
    {
        private int timeDifference;
        private int noOfBeer05;
        private int noOfBeer33;
        private int noOfWine;
        private int noOfStrongWine;
        private int noOfVodka;
        private int personID;

        private double totalGramOfAlc;
        private double weight;

        private string randomSex;
        private string personSex;



        public DrinkOverview()
        {

            InitializeComponent();
            personID = 0;
            this.DataContext = App._appViewModel;
        }

        private void DrinksContinue_Button_Click(object sender, RoutedEventArgs e)
        {
            double gramofAlcInBeer05 = 18;      //the amount of alcohol in each drink
            double gramOfAlcInBeer33 = 12.6;
            double gramOfAlcInWine = 14.4;
            double gramOfAlcInStrongWine = 13.2;
            double gramOfAlcInVodka = 0;
            double currentBloodAlcLevel = 0;
            //These figures are retrieved from http://nhi.no/livsstil/livsstil/alkohol/alkohol-fakta-14903.html?page=5

            //For kvinner: Alkohol i gram / (kroppsvekten i kg x 60%) - (0,15 x timer fra drikkestart) = promille
            //For menn: Alkohol i gram / (kroppsvekten i kg x 70%) - (0,15 x timer fra drikkestart) = promille

            if (personID > 0) //there doesn't exist an index 0
            {
                var person = from personTable in App._appViewModel.person
                             where personTable.personID == personID
                             select new { weight = personTable.personWeight, sex = personTable.personSex };

                foreach (var personWeight in person)
                {
                    weight = personWeight.weight;
                    personSex = personWeight.sex;
                }

            }
            else if (randomSex != null)
            {
                personSex = randomSex;
            }

            noOfBeer05 = Convert.ToInt32(BeerAmount_05.Text);   //convert the user input
            noOfBeer33 = Convert.ToInt32(BeerAmount_33cl.Text);
            noOfWine = Convert.ToInt32(WineAmount_15cl.Text);
            noOfStrongWine = Convert.ToInt32(StrongWine_75cl.Text);
            noOfVodka = Convert.ToInt32(VodkaAmount_4cl.Text);

            totalGramOfAlc = ((gramofAlcInBeer05 * noOfBeer05) + (gramOfAlcInBeer33 * noOfBeer33) + (gramOfAlcInStrongWine * noOfStrongWine) + (gramOfAlcInVodka * noOfVodka) + (gramOfAlcInWine * noOfWine));

            if (personSex == "Male")    //different absorbtion-rates for men and women
                currentBloodAlcLevel = ((totalGramOfAlc / (weight * 0.70)) - (0.15 * timeDifference));
            else if (personSex == "Female")
                currentBloodAlcLevel = ((totalGramOfAlc / (weight * 0.60)) - (0.15 * timeDifference));


            this.NavigationService.Navigate(new Uri("/ResultPage.xaml?bloodAlc=" + currentBloodAlcLevel, UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            noOfBeer05 = 0;
            noOfBeer33 = 0;
            noOfStrongWine = 0;
            noOfVodka = 0;
            noOfWine = 0;

            string tempDifference = null;
            string tempID = null;
            string tempWeight = null;
            string tempSex = null;
            

            NavigationContext.QueryString.TryGetValue("time", out tempDifference);
            NavigationContext.QueryString.TryGetValue("id", out tempID);
            NavigationContext.QueryString.TryGetValue("weight", out tempWeight);
            NavigationContext.QueryString.TryGetValue("sex", out tempSex);
                
            personID = Convert.ToInt32(tempID);
            timeDifference = Convert.ToInt32(tempDifference);
            weight = Convert.ToDouble(tempWeight);
            randomSex = tempSex;

            
            base.OnNavigatedTo(e);
        }

        

        #region GotFocus functions

        private void BeerAmount_05_GotFocus(object sender, RoutedEventArgs e)
        {
            BeerAmount_05.Text = "";
        }

        private void BeerAmount_33cl_GotFocus(object sender, RoutedEventArgs e)
        {
            BeerAmount_33cl.Text = "";
        }

        private void WineAmount_15cl_GotFocus(object sender, RoutedEventArgs e)
        {
            WineAmount_15cl.Text = "";
        }

        private void StrongWine_75cl_GotFocus(object sender, RoutedEventArgs e)
        {
            StrongWine_75cl.Text = "";    
        }

        private void VodkaAmount_4cl_GotFocus(object sender, RoutedEventArgs e)
        {
            VodkaAmount_4cl.Text = "";
        }

        #endregion//region for input focus

        #region LostFocus functions

        private void BeerAmount_05_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BeerAmount_05.Text == "")
            {
                BeerAmount_05.Text = "0";
            }
        }

        private void BeerAmount_33cl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BeerAmount_33cl.Text == "")
            {
                BeerAmount_33cl.Text = "0";
            }
        }

        private void WineAmount_15cl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (WineAmount_15cl.Text == "")
            {
                WineAmount_15cl.Text = "0";
            }
        }

        private void StrongWine_75cl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (StrongWine_75cl.Text == "")
            {
                StrongWine_75cl.Text = "0";
            }
        }

        private void VodkaAmount_4cl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (VodkaAmount_4cl.Text == "")
            {
                VodkaAmount_4cl.Text = "0";
            }
        }

        #endregion

    }
}