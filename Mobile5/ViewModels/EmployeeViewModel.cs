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
    public class EmployeeViewModel : INotifyPropertyChanged // Наш класс моделек.
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Создаем ивент.

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") // Функция вызова ивента.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Employee> _Employees; // Приватную переменную _workers (для внутренних преобразований).
        public IEnumerable<Employee> Employees // Публичную переменную workers (для внешних преобразований).
        {
            get { return _Employees; }
            set { _Employees = value; OnPropertyChanged(); } // Присваиваем внутренней переменной значение и вызываем ивент.
        }

        private void GetEmployee() // Присваиваем внешней переменной значение и преобразуем ее в лист.
        {
            using (var context = new Rchtest1Context()) { Employees = context.Employees.ToList(); }
        }

        public EmployeeViewModel() // Вызываем наши функции. 
        {
            newEmployee = new Employee();
            GetEmployee();
        }

        private Employee _newEmployee; // Приватную переменную _newUser (для внутренних преобразований).
        public Employee newEmployee // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _newEmployee; }
            set { _newEmployee = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }

        public DelegateCommand AddEmployeeCommand // Создаем команду для добавления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddEmployee();
                });
            }
        }

        private void AddEmployee() // Функция добавления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
                Employees = context.Employees.ToList();
            }
            newEmployee = new Employee();
        }

        private void DeleteEmployee(int EmployeeId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                Employee EmployeeToDelete = context.Employees.Find(EmployeeId);
                context.Employees.Remove(EmployeeToDelete);
                context.SaveChanges();
                Employees = context.Employees.ToList();
            }
        }

        public DelegateCommand DeleteEmployeeCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteEmployee((int)o);
                });
            }
        }

        private Employee _editEmployee; // Приватную переменную _newUser (для внутренних преобразований).
        public Employee editEmployee // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _editEmployee; }
            set { _editEmployee = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }
        private void UseEmployee(int EmployeeId)
        {
            using (var context = new Rchtest1Context())
            {
                editEmployee = context.Employees.Find(EmployeeId);
            }
        }

        public DelegateCommand UseEmployeeCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseEmployee((int)o);
                });
            }
        }

        private void EditEmployee(int EmployeeId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Employees.Update(editEmployee);
                context.SaveChanges();
                Employees = context.Employees.ToList();
            }
        }

        public DelegateCommand EditEmployeeCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditEmployee((int)o);
                });
            }
        }

    }
}
