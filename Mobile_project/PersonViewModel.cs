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
            personDB = new DatabaseContext(personDBConnectionString);
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

        public void SaveChangesToDB()
        {
            personDB.SubmitChanges();
        }

        public void LoadFromDatabase()
        {
            var personItemsInDB = from PersonData personsInTable in personDB.PersonDataTable
                                  select personsInTable;

            person = new ObservableCollection<PersonData>(personItemsInDB);
        }

        public void AddPerson(PersonData newPerson)
        {
            person.Add(newPerson);

            personDB.PersonDataTable.InsertOnSubmit(newPerson);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
