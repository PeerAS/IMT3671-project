using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class DatabaseContext : DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/WhenCanIDrink.sdf";

        public DatabaseContext(string connectionString) : base(connectionString)
        { }

        public Table<PersonData> PersonDataTable;
    }

    [Table]
    public class PersonData : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _personID;

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int personID
        {
            get
            {
                return _personID;
            }
            set
            {
                if (_personID != value)
                {
                    NotifyPropertyChanging("personID");
                    _personID = value;
                    NotifyPropertyChanged("personID");
                }
            }
        }

        private string _personName;
        [Column]
        public string personName
        {
            get
            {
                return _personName;
            }
            set
            {
                if (_personName != value)
                {
                    NotifyPropertyChanging("personName");
                    _personName = value;
                    NotifyPropertyChanged("personName");
                }
            }
        }

        private double _personWeight;
        [Column]
        public double personWeight
        {
            get
            {
                return _personWeight;
            }
            set
            {
                if (_personWeight != value)
                {
                    NotifyPropertyChanging("personWeight");
                    _personWeight = value;
                    NotifyPropertyChanged("personWeight");
                }
            }
        }

        private string _personSex;
        [Column]
        public string personSex
        {
            get
            {
                return _personSex;
            }
            set
            {
                if (_personSex != value)
                {
                    NotifyPropertyChanging("personSex");
                    _personSex = value;
                    NotifyPropertyChanged("personSex");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }
    }

}
