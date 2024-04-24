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
    public class PositionViewModel : INotifyPropertyChanged // Наш класс моделек.
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Создаем ивент.

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") // Функция вызова ивента.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Position> _Positions; // Приватную переменную _workers (для внутренних преобразований).
        public IEnumerable<Position> Positions // Публичную переменную workers (для внешних преобразований).
        {
            get { return _Positions; }
            set { _Positions = value; OnPropertyChanged(); } // Присваиваем внутренней переменной значение и вызываем ивент.
        }

        private void GetPosition() // Присваиваем внешней переменной значение и преобразуем ее в лист.
        {
            using (var context = new Rchtest1Context()) { Positions = context.Positions.ToList(); }
        }

        public PositionViewModel() // Вызываем наши функции. 
        {
            newPosition = new Position();
            GetPosition();
        }

        private Position _newPosition; // Приватную переменную _newUser (для внутренних преобразований).
        public Position newPosition // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _newPosition; }
            set { _newPosition = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }

        public DelegateCommand AddPositionCommand // Создаем команду для добавления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddPosition();
                });
            }
        }

        private void AddPosition() // Функция добавления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Positions.Add(newPosition);
                context.SaveChanges();
                Positions = context.Positions.ToList();
            }
            newPosition = new Position();
        }

        private void DeletePosition(int PositionId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                Position PositionToDelete = context.Positions.Find(PositionId);
                context.Positions.Remove(PositionToDelete);
                context.SaveChanges();
                Positions = context.Positions.ToList();
            }
        }

        public DelegateCommand DeletePositionCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeletePosition((int)o);
                });
            }
        }

        private Position _editPosition; // Приватную переменную _newUser (для внутренних преобразований).
        public Position editPosition // Публичную переменную newUser (для внешних преобразований).
        {
            get { return _editPosition; }
            set { _editPosition = value; OnPropertyChanged(); } // Присваиваем внешней переменной значение и преобразуем ее в лист.
        }
        private void UsePosition(int PositionId)
        {
            using (var context = new Rchtest1Context())
            {
                editPosition = context.Positions.Find(PositionId);
            }
        }

        public DelegateCommand UsePositionCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UsePosition((int)o);
                });
            }
        }

        private void EditPosition(int PositionId) // Функция удаления пользователя.
        {
            using (var context = new Rchtest1Context())
            {
                context.Positions.Update(editPosition);
                context.SaveChanges();
                Positions = context.Positions.ToList();
            }
        }

        public DelegateCommand EditPositionCommand // Создаем команду для удаления пользователя, вызывающую функцию добавления пользователя.
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditPosition((int)o);
                });
            }
        }

    }
}

