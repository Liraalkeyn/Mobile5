using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mobile5.Context;
using Mobile5.Models;

namespace Mobile5.ViewModels
{
    public class CityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<City> _cities;
        public IEnumerable<City> cities
        {
            get { return _cities; }
            set { _cities = value; OnPropertyChanged(); }
        }

        private void GetCity() 
        {
            using (var context = new Rchtest1Context()) { cities = context.Cities.ToList(); }
        }

        public CityViewModel()
        {
            newCity = new City();
            GetCity();
        }

        private City _newCity; 
        public City newCity 
        {
            get { return _newCity; }
            set { _newCity = value; OnPropertyChanged(); } 
        }

        public DelegateCommand AddCityCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddCity();
                });
            }
        }

        private void AddCity()
        {
            using (var context = new Rchtest1Context())
            {
                context.Cities.Add(newCity);
                context.SaveChanges();
                cities = context.Cities.ToList();
            }
            newCity = new City();
        }

        private void DeleteCity(int CityId)
        {
            using (var context = new Rchtest1Context())
            {
                City CityToDelete = context.Cities.Find(CityId);
                context.Cities.Remove(CityToDelete);
                context.SaveChanges();
                cities = context.Cities.ToList();
            }
        }

        public DelegateCommand DeleteCityCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteCity((int)o);
                });
            }
        }

        private City _editCity; 
        public City editCity 
        {
            get { return _editCity; }
            set { _editCity = value; OnPropertyChanged(); } 
        }
        private void UseCity(int CityId)
        {
            using (var context = new Rchtest1Context())
            {
                editCity = context.Cities.Find(CityId);
            }
        }

        public DelegateCommand UseCityCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseCity((int)o);
                });
            }
        }

        private void EditCity(int CityId)
        {
            using (var context = new Rchtest1Context())
            {
                context.Cities.Update(editCity);
                context.SaveChanges();
                cities = context.Cities.ToList();
            }
        }

        public DelegateCommand EditCityCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditCity((int)o);
                });
            }
        }

    }
}

