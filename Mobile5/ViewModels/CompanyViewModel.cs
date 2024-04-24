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
    public class CompanyViewModel : INotifyPropertyChanged // Наш класс моделек.
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Создаем ивент.

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") // Функция вызова ивента.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Company> _Companies; // Приватную переменную _workers (для внутренних преобразований).
        public IEnumerable<Company> Companies // Публичную переменную workers (для внешних преобразований).
        {
            get { return _Companies; }
            set { _Companies = value; OnPropertyChanged(); } // Присваиваем внутренней переменной значение и вызываем ивент.
        }

        private void GetCompany() // Присваиваем внешней переменной значение и преобразуем ее в лист.
        {
            using (var context = new Rchtest1Context()) { Companies = context.Companies.ToList(); }
        }

        public CompanyViewModel() // Вызываем наши функции. 
        {
            newCompany = new Company();
            GetCompany();
        }

        private Company _newCompany; // Приватную переменную _newUser (для внутренних преобразований).
        public Company newCompany // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _newCompany; }
            set { _newCompany = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }

        public DelegateCommand AddCompanyCommand // Создаем команду для добавления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddCompany();
                });
            }
        }

        private void AddCompany() // Функция добавления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Companies.Add(newCompany);
                context.SaveChanges();
                Companies = context.Companies.ToList();
            }
            newCompany = new Company();
        }

        private void DeleteCompany(int CompanyId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                Company CompanyToDelete = context.Companies.Find(CompanyId);
                context.Companies.Remove(CompanyToDelete);
                context.SaveChanges();
                Companies = context.Companies.ToList();
            }
        }

        public DelegateCommand DeleteCompanyCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteCompany((int)o);
                });
            }
        }

        private Company _editCompany; // Приватную переменную _newUser (для внутренних преобразований).
        public Company editCompany // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _editCompany; }
            set { _editCompany = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }
        private void UseCompany(int CompanyId)
        {
            using (var context = new Rchtest1Context())
            {
                editCompany = context.Companies.Find(CompanyId);
            }
        }

        public DelegateCommand UseCompanyCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseCompany((int)o);
                });
            }
        }

        private void EditCompany(int CompanyId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Companies.Update(editCompany);
                context.SaveChanges();
                Companies = context.Companies.ToList();
            }
        }

        public DelegateCommand EditCompanyCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditCompany((int)o);
                });
            }
        }

    }
}
