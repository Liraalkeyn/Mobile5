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
    public class TransportViewModel : INotifyPropertyChanged // Наш класс моделек.
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Создаем ивент.

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") // Функция вызова ивента.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Transport> _Transports; // Приватную переменную _workers (для внутренних преобразований).
        public IEnumerable<Transport> Transports // Публичную переменную workers (для внешних преобразований).
        {
            get { return _Transports; }
            set { _Transports = value; OnPropertyChanged(); } // Присваиваем внутренней переменной значение и вызываем ивент.
        }

        private void GetTransport() // Присваиваем внешней переменной значение и преобразуем ее в лист.
        {
            using (var context = new Rchtest1Context()) { Transports = context.Transports.ToList(); }
        }

        public TransportViewModel() // Вызываем наши функции. 
        {
            newTransport = new Transport();
            GetTransport();
        }

        private Transport _newTransport; // Приватную переменную _newUser (для внутренних преобразований).
        public Transport newTransport // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _newTransport; }
            set { _newTransport = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }

        public DelegateCommand AddTransportCommand // Создаем команду для добавления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddTransport();
                });
            }
        }

        private void AddTransport() // Функция добавления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Transports.Add(newTransport);
                context.SaveChanges();
                Transports = context.Transports.ToList();
            }
            newTransport = new Transport();
        }

        private void DeleteTransport(int TransportId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                Transport TransportToDelete = context.Transports.Find(TransportId);
                context.Transports.Remove(TransportToDelete);
                context.SaveChanges();
                Transports = context.Transports.ToList();
            }
        }

        public DelegateCommand DeleteTransportCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteTransport((int)o);
                });
            }
        }

        private Transport _editTransport; // Приватную переменную _newUser (для внутренних преобразований).
        public Transport editTransport // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _editTransport; }
            set { _editTransport = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }
        private void UseTransport(int TransportId)
        {
            using (var context = new Rchtest1Context())
            {
                editTransport = context.Transports.Find(TransportId);
            }
        }

        public DelegateCommand UseTransportCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseTransport((int)o);
                });
            }
        }

        private void EditTransport(int TransportId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Transports.Update(editTransport);
                context.SaveChanges();
                Transports = context.Transports.ToList();
            }
        }

        public DelegateCommand EditTransportCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditTransport((int)o);
                });
            }
        }

    }
}


