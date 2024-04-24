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
    public class CityViewModel : INotifyPropertyChanged // Наш класс моделек.
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Создаем ивент.

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") // Функция вызова ивента.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<City> _cities; // Приватную переменную _workers (для внутренних преобразований).
        public IEnumerable<City> cities // Публичную переменную workers (для внешних преобразований).
        {
            get { return _cities; }
            set { _cities = value; OnPropertyChanged(); } // Присваиваем внутренней переменной значение и вызываем ивент.
        }

        private void GetCity() // Присваиваем внешней переменной значение и преобразуем ее в лист.
        {
            using (var context = new Rchtest1Context()) { cities = context.Cities.ToList(); }
        }

        public CityViewModel() // Вызываем наши функции. 
        {
            newCity = new City();
            GetCity();
        }

        private City _newCity; // Приватную переменную _newUser (для внутренних преобразований).
        public City newCity // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _newCity; }
            set { _newCity = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }

        public DelegateCommand AddCityCommand // Создаем команду для добавления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddCity();
                });
            }
        }

        private void AddCity() // Функция добавления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Cities.Add(newCity);
                context.SaveChanges();
                cities = context.Cities.ToList();
            }
            newCity = new City();
        }

        private void DeleteCity(int CityId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                City CityToDelete = context.Cities.Find(CityId);
                context.Cities.Remove(CityToDelete);
                context.SaveChanges();
                cities = context.Cities.ToList();
            }
        }

        public DelegateCommand DeleteCityCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteCity((int)o);
                });
            }
        }

        private City _editCity; // Приватную переменную _newUser (для внутренних преобразований).
        public City editCity // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _editCity; }
            set { _editCity = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }
        private void UseCity(int CityId)
        {
            using (var context = new Rchtest1Context())
            {
                editCity = context.Cities.Find(CityId);
            }
        }

        public DelegateCommand UseCityCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseCity((int)o);
                });
            }
        }

        private void EditCity(int CityId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Cities.Update(editCity);
                context.SaveChanges();
                cities = context.Cities.ToList();
            }
        }

        public DelegateCommand EditCityCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
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

