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
    public class TypeCargoViewModel : INotifyPropertyChanged // Наш класс моделек.
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Создаем ивент.

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") // Функция вызова ивента.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<TypeCargo> _TypeCargos; // Приватную переменную _workers (для внутренних преобразований).
        public IEnumerable<TypeCargo> TypeCargos // Публичную переменную workers (для внешних преобразований).
        {
            get { return _TypeCargos; }
            set { _TypeCargos = value; OnPropertyChanged(); } // Присваиваем внутренней переменной значение и вызываем ивент.
        }

        private void GetTypeCargo() // Присваиваем внешней переменной значение и преобразуем ее в лист.
        {
            using (var context = new Rchtest1Context()) { TypeCargos = context.TypeCargos.ToList(); }
        }

        public TypeCargoViewModel() // Вызываем наши функции. 
        {
            newTypeCargo = new TypeCargo();
            GetTypeCargo();
        }

        private TypeCargo _newTypeCargo; // Приватную переменную _newUser (для внутренних преобразований).
        public TypeCargo newTypeCargo // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _newTypeCargo; }
            set { _newTypeCargo = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }

        public DelegateCommand AddTypeCargoCommand // Создаем команду для добавления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddTypeCargo();
                });
            }
        }

        private void AddTypeCargo() // Функция добавления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.TypeCargos.Add(newTypeCargo);
                context.SaveChanges();
                TypeCargos = context.TypeCargos.ToList();
            }
            newTypeCargo = new TypeCargo();
        }

        private void DeleteTypeCargo(int TypeCargoId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                TypeCargo TypeCargoToDelete = context.TypeCargos.Find(TypeCargoId);
                context.TypeCargos.Remove(TypeCargoToDelete);
                context.SaveChanges();
                TypeCargos = context.TypeCargos.ToList();
            }
        }

        public DelegateCommand DeleteTypeCargoCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteTypeCargo((int)o);
                });
            }
        }

        private TypeCargo _editTypeCargo; // Приватную переменную _newUser (для внутренних преобразований).
        public TypeCargo editTypeCargo // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _editTypeCargo; }
            set { _editTypeCargo = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }
        private void UseTypeCargo(int TypeCargoId)
        {
            using (var context = new Rchtest1Context())
            {
                editTypeCargo = context.TypeCargos.Find(TypeCargoId);
            }
        }

        public DelegateCommand UseTypeCargoCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseTypeCargo((int)o);
                });
            }
        }

        private void EditTypeCargo(int TypeCargoId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.TypeCargos.Update(editTypeCargo);
                context.SaveChanges();
                TypeCargos = context.TypeCargos.ToList();
            }
        }

        public DelegateCommand EditTypeCargoCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditTypeCargo((int)o);
                });
            }
        }

    }
}



