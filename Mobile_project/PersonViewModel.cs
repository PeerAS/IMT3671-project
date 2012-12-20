using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using Database.Model;

namespace Mobile_project.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private DatabaseContext personDB;

        public PersonViewModel(string personDBConnectionString)
        {
            personDB = new DatabaseContext(personDBConnectionString);   //creates a new database with a connection
        }

        private ObservableCollection<PersonData> _person;
        public ObservableCollection<PersonData> person
        {
            get
            {
                return _person;
            }
            set
            {
                if (_person != value)
                {
                    _person = value;
                    NotifyPropertyChanged("person");
                }
            }
        }

        public void SaveChangesToDB()   //save changes to the database
        {
            personDB.SubmitChanges();
        }

        public void LoadFromDatabase()  //get all the items from the database and put them in an observable collection
        {
            var personItemsInDB = from PersonData personsInTable in personDB.PersonDataTable
                                  select personsInTable;

            person = new ObservableCollection<PersonData>(personItemsInDB);
        }

        public void AddPerson(PersonData newPerson) //add a new person
        {
            person.Add(newPerson);

            personDB.PersonDataTable.InsertOnSubmit(newPerson);
            
        }

        public void UpdatePerson(int personID)  //doesn't work yet
        {
            IQueryable<PersonData> test = from PersonData personExist in personDB.PersonDataTable
                                        where personExist.personID == personID
                                        select personExist;

            
        }

        public void DeletePerson(int personID)
        {
           IQueryable<PersonData> test = from PersonData personToDelete in personDB.PersonDataTable
                                         where personToDelete.personID == personID
                                         select personToDelete;
           
            
            person.Remove(test.First());
            personDB.PersonDataTable.DeleteOnSubmit(test.First());

            personDB.SubmitChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
